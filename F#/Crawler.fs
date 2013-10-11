open System.Collections.Generic
open System
open System.Net
open System.IO
open System.Threading
open System.Text.RegularExpressions
open HtmlAgilityPack
open FSharp.Control

type System.Net.WebClient with
    member x.AsyncDownloadData(url: string) = 
        Async.FromContinuations(fun (cont, econt, ccont) ->
                                let handler = new DownloadDataCompletedEventHandler (fun sender args ->          
                                    if args.Cancelled then
                                        ccont (new OperationCanceledException()) 
                                    elif args.Error <> null then
                                        econt args.Error
                                    else
                                        cont (args.Result))
                                x.DownloadDataCompleted.AddHandler(handler)
                                x.DownloadDataAsync(new Uri(url)))


let downloadDocument url = async {
  try let wc = new WebClient()
      let! html = wc.AsyncDownloadString(Uri(url))
      let doc = new HtmlDocument()
      doc.LoadHtml(html)
      return Some doc 
  with _ -> return None }

let extractLinks (doc:HtmlDocument) = 
  try
    [ for a in doc.DocumentNode.SelectNodes("//a") do
        if a.Attributes.Contains("href") then
          let href = a.Attributes.["href"].Value
          if href.StartsWith("http://") then 
            let endl = href.IndexOf('?')
            yield if endl > 0 then href.Substring(0, endl) else href ]
  with _ -> []

let extractPics (doc:HtmlDocument) = 
  try
    [ for a in doc.DocumentNode.SelectNodes("//img") do
       let ref = a.Attributes.["src"].Value
       if ref.EndsWith(".jpg") || ref.EndsWith(".png") || ref.EndsWith(".gif") then
        yield a.Attributes.["src"].Value ]
  with _ -> []


let downloadFileAsync (sourceUrl:string) =
    async { 
            let wc = new WebClient()
            let! response = wc.AsyncDownloadData(sourceUrl)
            let filename = Path.GetFileName(sourceUrl)
            printfn "%A" filename
            File.WriteAllBytes(filename, response) 
            }
let downloadFile (sourceUrl:string) =
             let wc = new WebClient()
             let filename = Path.GetFileName(sourceUrl)
             let response = wc.DownloadData(new Uri(sourceUrl))
             printfn "%A" filename
             File.WriteAllBytes(filename, response) 

let rec Crawler url = 
  let visited = new System.Collections.Generic.HashSet<_>()
  let rec loop url = asyncSeq {
    if visited.Add(url) then
      let! doc = downloadDocument url
      match doc with 
      | Some doc ->
          for piclink in extractPics doc do
             yield piclink
          for link in extractLinks doc do
            yield! loop link 
      | _ -> () }
  loop url

Crawler "http://news.bing.com"
//|> AsyncSeq.take 100
//|> AsyncSeq.iterAsync downloadFileAsync
|> AsyncSeq.iter downloadFile
|> Async.StartImmediate

