// batov 271gr spbu. 2013.

module OO.Main
open System

type Product (theName:string,theCost:int,thePack:string,thePlace:int,theAmount:int) = 
    let mutable name = theName
    let mutable cost = theCost
    let mutable packaging = thePack
    let mutable place = thePlace
    let mutable amount = theAmount
    member this.Name() = name
    member this.Cost() = cost
    member this.Place() = place
    member this.Amount() = amount  
    member this.reduceAmount(count) =
      let d = amount-count 
      if (d >= 0) 
        then 
           amount <- d
           true
        else 
           false
    
type FoodProduct (theName:string,theCost:int,thePack:string,thePlace:int,theAmount:int) = 
    inherit Product (theName,theCost,thePack,thePlace,theAmount)

   
type HouseholdProduct (theName:string,theCost:int,thePack:string,thePlace:int,theAmount:int) = 
    inherit Product (theName,theCost,thePack,thePlace,theAmount)

type Equipment (theName:string,theCondition:int, theCost:int) = 
    let mutable name = theName
    let mutable condition = theCondition
    let mutable cost = theCost
    member this.Name = name
    member this.Condition = condition
    member this.Cost = cost
    
type CashBox (theName:string,theCondition:int, theCost:int) = 
    inherit Equipment (theName,theCondition, theCost)
    let mutable theAmountOfMoney = 0
    member this.Cash = theAmountOfMoney
    member this.PutCash(cash) = theAmountOfMoney <- theAmountOfMoney + cash
    member this.GetCash = 
       let t = theAmountOfMoney 
       theAmountOfMoney <- 0
       t
         
type Refrigerator (theName:string,theCondition:int, theCost:int) = 
    inherit Equipment (theName,theCondition, theCost)  
    let mutable temperature = 3
    member this.SetTemp(temp) = temperature <- temp
    member this.Temperature = temperature

type Staff (theName:string, theSalary:int, theStanding:int) =
    let mutable name = theName
    let mutable salary = theSalary
    let mutable standing = theStanding
    member this.Name = name
    member this.Salary = salary
    member this.SetSalary(thesalary) = salary <- thesalary
    member this.SetStanding(thesstanding) = standing <- thesstanding

type ProductionDepartment (theName:string, theSalary:int, theStanding:int, theClothes:string) = 
    inherit Staff (theName, theSalary, theStanding)
    let mutable clothes = theClothes
    member this.sell(prod:Product,count,theCashBox:CashBox) = 
     if prod.reduceAmount(count)
      then
        let t = prod.Cost()
        theCashBox.PutCash(t)
      
                                                                                                                                  
type FoodSeller (theName:string, theSalary:int, theStanding:int, theClothes:string) =
    inherit ProductionDepartment (theName, theSalary, theStanding, theClothes)
    
type HouseholdAppliancesSeller (theName:string, theSalary:int, theStanding:int, theClothes:string) =
    inherit ProductionDepartment (theName, theSalary, theStanding, theClothes)  

type Chiefs (theName:string, theSalary:int, theStanding:int) =
    inherit Staff (theName, theSalary, theStanding)
    
type Head (theName:string, theSalary:int, theStanding:int) =
    inherit Chiefs(theName, theSalary, theStanding)
    member this.SetSalary(man:Staff ,salary) = 
      man.SetSalary(salary)    
    
type Accountant (theName:string, theSalary:int, theStanding:int) =
    inherit Chiefs(theName, theSalary, theStanding)
    member this.TakeCash(theCashBox:CashBox) = theCashBox.GetCash
          
type idCard () =
    let mutable id = 1
    let mutable condition = 5