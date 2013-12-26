open System
open System.Net
open System.Xml
open System.IO
open Microsoft.FSharp.Control.WebExtensions
open System.Drawing
open System.Windows.Forms
open HtmlAgilityPack
open System.Collections.Concurrent
open FSharp.Control

let visited = new ConcurrentDictionary<string, bool>()
let downloaded = new ConcurrentDictionary<string, bool>()

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


let downloadFileAsync (sourceUrl:string) =
    async { 
            let wc = new WebClient()
            let! response = wc.AsyncDownloadData(sourceUrl)
            let filename = Path.GetFileName(sourceUrl)
            printfn "new file = %A" filename
            File.WriteAllBytes(filename, response) 
          }


let extractLinks (doc:HtmlDocument) = 
    try
        [ for nd in doc.DocumentNode.SelectNodes("//a[@href]") do
            if nd.Attributes.["href"].Value.StartsWith("http://") then yield nd.Attributes.["href"].Value ]
    with _ -> []

let extractPics (doc:HtmlDocument) = 
    try
        [ for nd in doc.DocumentNode.SelectNodes("//img") do
            let src = nd.Attributes.["src"].Value
            if src.EndsWith(".jpg") || src.EndsWith(".jpeg") || src.EndsWith(".png") || src.EndsWith(".gif")   then yield src ]
    with _ -> []

let downloadDocument (url:string) = 
    async {
    try
        let webClient = new WebClient()
        let! html = webClient.AsyncDownloadString(Uri(url))
        let doc = new HtmlDocument()
        doc.LoadHtml(html)
        return Some doc
    with
        | _ -> return None
    }

let rec Crawler(url:string) = asyncSeq {
    if not(visited.ContainsKey(url)) 
      then
        visited.GetOrAdd(url, true) |> ignore 
        let! doc = downloadDocument url
        match doc with
        | Some doc ->
            for pic in extractPics doc do 
                yield pic 
            for link in extractLinks doc do
                yield! Crawler link 
        | _ -> ()
}



Crawler "http://www.bing.com"
    |> AsyncSeq.take 10
    |> AsyncSeq.iterAsync downloadFileAsync
    |> Async.Start

#if COMPILED
[<STAThread()>]
 Application.Run()
#endif

