
module RE.Main

open System.Text.RegularExpressions
open System

let user_name = "^[a-zA-Z0-9#!&'~|$*_+/=?%^\.`}{]+(\.[a-zA-Z0-9#!&'~|$*_+/=?%^`}{]]+)*"
let domain_name = "([a-zA-Z0-9]([a-zA-Z0-9]{0,61}[a-zA-Z0-9])?\.)*(aero|arpa|asia|biz|cat|com|coop|edu|gov|info|int|jobs|mil|mobi|museum|name|net|org|pro|tel|travel|xxx|[a-zA-Z][a-zA-Z])$"
let pattern = user_name + "@" + domain_name

let regexp = new Regex(pattern) 
let matcher input = regexp.IsMatch(input)  

let tester = 
  if matcher "niceandsimple@example.com" = true then printfn "%A" "OK" else printfn "%A" "FAILED"
  if matcher "!#$@example.org" = true then printfn "%A" "OK" else printfn "%A" "FAILED"  
  if matcher "!#$%&'*+/=?^_`{}|~a@example.org" = true then printfn "%A" "OK" else printfn "%A" "FAILED"  
  if matcher "Abc.example.com" = false then printfn "%A" "OK" else printfn "%A" "FAILED"  
  if matcher "A@b@c@example.com" = false then printfn "%A" "OK" else printfn "%A" "FAILED"  
  if matcher "ab(c)d,e:f;g<h>i[j\k]l@example.com" = false then printfn "%A" "OK" else printfn "%A" "FAILED"  
  if matcher "this\ still\"not\\allowed@example.com" = false then printfn "%A" "OK" else printfn "%A" "FAILED"  
  if matcher "alittl.elengthyb.SDFutfine@example.net" = true then printfn "%A" "OK" else printfn "%A" "FAILED"    
  if matcher "postbox@com" = true then printfn "%A" "OK" else printfn "%A" "FAILED" 
   