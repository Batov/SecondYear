//module RE.Main

open System.Text.RegularExpressions
open System
open Microsoft.VisualStudio.TestTools.UnitTesting
  
let user_name = "^[a-zA-Z0-9#!&'~|$*_+/=?%^\.`}{]+(\.[a-zA-Z0-9#!&'~|$*_+/=?%^`}{]]+)*"
let domain_name = "([a-zA-Z0-9]([a-zA-Z0-9]{0,61}[a-zA-Z0-9])?\.)*(aero|arpa|asia|biz|cat|com|coop|edu|gov|info|int|jobs|mil|mobi|museum|name|net|org|pro|tel|travel|xxx|[a-zA-Z][a-zA-Z])$"
let pattern = user_name + "@" + domain_name

let regexp = new Regex(pattern) 
let matcher input = regexp.IsMatch(input)
l

[<TestClass>]
type UnitTest() =
    [<TestMethod>]
    member x.TestMethod1 () =
        let testVal = matcher "niceandsimple@example.com"
        Assert.IsTrue(testVal)
    [<TestMethod>]
    member x.TestMethod2 () =
        let testVal = matcher "!#$@example.org"
        Assert.IsTrue(testVal)
    [<TestMethod>]
    member x.TestMethod3 () =
        let testVal = matcher "ab(c)d,e:f;g<h>i[j\k]l@example.com"
        Assert.IsFalse(testVal)
    [<TestMethod>]
    member x.TestMethod4 () =
        let testVal = matcher "this\ still\"not\\allowed@example.com"
        Assert.IsFalse(testVal)


