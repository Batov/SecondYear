module RE.Main

open System.Text.RegularExpressions
open System

let user_name = "^[-a-z!#$%&'*+/=?^_`{|}~]+(\.[-a-z0-9!#$%&'*+/=?^_`{|}~]+)*"
let domain_name = "([a-z0-9]([a-z0-9]{0,61}[a-zA-Z0-9])?\.)*(aero|arpa|asia|biz|cat|com|coop|edu|gov|info|int|jobs|mil|mobi|museum|name|net|org|pro|tel|travel|xxx|[a-z][a-z])$"
let pattern = user_name + "@" + domain_name

let regexp = new Regex(pattern) 
let matcher input = regexp.IsMatch(input)  

let tester = 
  if matcher "a@b.cc" = true then printfn "%A" "OK" else printfn "%A" "FAILED"
  if matcher "victor.polozov@mail.ru" = true then printfn "%A" "OK" else printfn "%A" "FAILED"  
  if matcher "my@domain.info" = true then printfn "%A" "OK" else printfn "%A" "FAILED"  
  if matcher "_.1@mail.com" = true then printfn "%A" "OK" else printfn "%A" "FAILED"  
  if matcher "coins_department@hermitage.museum" = true then printfn "%A" "OK" else printfn "%A" "FAILED"  
  
  if matcher "a@b.c" = false then printfn "%A" "OK" else printfn "%A" "FAILED"  
  if matcher "a..b@mail.ru" = false then printfn "%A" "OK" else printfn "%A" "FAILED"  
  if matcher ".a@mail.ru" = false then printfn "%A" "OK" else printfn "%A" "FAILED"    
  if matcher "yo@domain.somedomain" = false then printfn "%A" "OK" else printfn "%A" "FAILED" 
  if matcher "1@mail.ru" = false then printfn "%A" "OK" else printfn "%A" "FAILED"    
  
  [<TestClass>]
   type UnitTest() =
    [<TestMethod>]
    member x.TestMethod1 () =
        let testVal = matcher "a@b.cc"
        Assert.IsTrue(testVal)
    [<TestMethod>]
    member x.TestMethod2 () =
        let testVal = matcher "victor.polozov@mail.ru"
        Assert.IsTrue(testVal)
    [<TestMethod>]
    member x.TestMethod3 () =
        let testVal = matcher "my@domain.info"
        Assert.IsTrue(testVal)
    [<TestMethod>]
    member x.TestMethod4 () =
        let testVal = matcher "_.1@mail.com"
        Assert.IsTrue(testVal)
    [<TestMethod>]
    member x.TestMethod5 () =
        let testVal = matcher "coins_department@hermitage.museum"
        Assert.IsTrue(testVal)
    [<TestMethod>]
    member x.TestMethod6 () =
        let testVal = matcher "a@b.c"
        Assert.IsFalse(testVal)
    [<TestMethod>]
    member x.TestMethod7 () =
        let testVal = matcher "a..b@mail.ru"
        Assert.IsFalse(testVal)
    [<TestMethod>]
    member x.TestMethod8 () =
        let testVal = matcher ".a@mail.ru"
        Assert.IsFalse(testVal)
    [<TestMethod>]
    member x.TestMethod9 () =
        let testVal = matcher "yo@domain.somedomain"
        Assert.IsFalse(testVal)
    [<TestMethod>]
    member x.TestMethod10 () =
        let testVal = matcher "1@mail.ru"
        Assert.IsFalse(testVal)
