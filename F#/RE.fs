module RE.Main

open System.Text.RegularExpressions
open System

let user_name = "^[-a-zA-Z!#$%&'*+/=?^_`{|}~]+(\.[-a-zA-Z0-9!#$%&'*+/=?^_`{|}~]+)*"
let domain_name = "([a-z0-9]([a-z0-9]{0,61}[a-zA-Z0-9])?\.)*(aero|arpa|asia|biz|cat|com|coop|edu|gov|info|int|jobs|mil|mobi|museum|name|net|org|pro|tel|travel|xxx|[a-z][a-z])$"
let pattern = user_name + "@" + domain_name

let regexp = new Regex(pattern) 
let matcher input = regexp.IsMatch(input) 
let tester input ans = if (matcher input) <> ans then printf "%A parser failed! Should be %A \n" input ans   

tester "Victor.Polozov@gmail.ru" true
tester "a@b.cc" true
tester "mY@domain.info" true
tester "_.1@mail.com" true
tester "coins_department@hermitage.museum" true

tester "a@b.c" false
tester "abracadabra@B.com" false
tester "a..b@mail.ru" false
tester ".a@mail.ru" false
tester "yo@domain.somedomain" false
tester "1@mail.ru" false

printfn "Testing is finished..."

(* 
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
        Assert.IsFalse(testVal) *)
