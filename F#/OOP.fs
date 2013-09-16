// batov 271gr spbu. 2013.

module OO.Main
open System

type Product () = 
    let mutable name = "milk"
    let mutable cost = 0
    let mutable packaging = "bottle"
    let mutable place = "345"
    let mutable amount = 10
    member this.GetCost() = cost
    member this.reduceAmount(count) = 
      if ((amount-count) >= 0) 
        then 
           amount <- amount - count
           true
        else 
           false
    
type FoodProduct () = 
    inherit Product ()

   
type HouseholdProduct () = 
    inherit Product ()

type Equipment () = 
    let mutable condition = 5
    let mutable cost = 0
    
type CashBox () = 
    inherit Equipment ()
    let mutable theAmountOfMoney = 0
    member this.PutCash(cash) = theAmountOfMoney <- theAmountOfMoney + cash
    member this.GetCash = 
       let t = theAmountOfMoney 
       theAmountOfMoney <- 0
       t
         
type Refrigerator () = 
    inherit Equipment ()  
    let mutable temperature = 3

type Staff () =
    let mutable name = "Name"
    let mutable salary = 20
    let mutable standing = 3
    member this.SetSalary(thesalary) = salary <- thesalary
    member this.SetName(thename) = name <- thename
    member this.SetStanding(thesstanding) = standing <- thesstanding

type ProductionDepartment () = 
    inherit Staff ()
    let mutable clothes = "Empty"
    member this.sell(prod:Product,count,theCashBox:CashBox) = 
     if prod.reduceAmount(count)
      then
        let t = prod.GetCost
        theCashBox.PutCash(t)
      
                                                                                                                                  
type FoodSeller () =
    inherit ProductionDepartment ()
    
type HouseholdAppliancesSeller () =
    inherit ProductionDepartment ()  

type Chiefs () =
    inherit Staff ()
    
type Head () =
    inherit Chiefs()
    member this.SetSalary(man:Staff ,salary) = 
      man.SetSalary(salary)    
    
type Accountant () =
    inherit Chiefs()
    member this.TakeCash(theCashBox:CashBox) = theCashBox.GetCash
          
type idCard () =
    let mutable id = 0
    let mutable condition = 5


       
     