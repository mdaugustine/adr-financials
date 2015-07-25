// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.
open System.IO
open System

let values =
  "test.txt"
  |> File.ReadAllLines
  |> Array.mapi (fun i line -> line.Split([|'\t'|]))

let date = 
  let month = Int32.Parse(values.[1].[1].Split([|'/'|]).[0]) - 1
  let year = Int32.Parse(values.[1].[1].Split([|'/'|]).[2])
  if month < 1 then (12, year - 1) else (month, year)

let sales = Decimal.Parse(values.[2].[7])
let currency = values.[1].[8]
let app = values.[1].[4]

Console.WriteLine("month " + date.ToString())
Console.WriteLine("sales " + sales.ToString())
Console.WriteLine("currency " + currency)
Console.WriteLine("game " + app)

for i in values do
  for j in i do
     printfn "%s" j

[<EntryPoint>]
let main argv = 
    printfn "%A" argv
    0 // return an integer exit code