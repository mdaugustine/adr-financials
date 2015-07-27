// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.
open System.IO
open System
open FSharp.Data
open System.Text.RegularExpressions

(*
let ParseFile = 
    let ParseFile = File.ReadAllLines("test.txt")
    
    let ADarkRoom = 
        ParseFile
        |> Seq.filter(fun x -> if x.Contains("a-dark-room") then true else false)
    
        ParseFile
        |> Seq.filter(fun x -> if x.Contains("theensign") then true else false)

    let myFileSet = 
        let mySubFileSet = Seq.append ADarkRoom TheEnsign    
    let TheEnsign = 
        let FileSummary = "Number of Entries : " + ParseFile.Length.ToString() + " Number of Errors : " + (ADarkRoom |> Seq.length).ToString()
        Seq.append ( FileSummary |> Seq.singleton) mySubFileSet
    
    File.WriteAllLines("Test2.txt", myFileSet)        

ParseFile

  
            |> File.ReadLines
            |> Seq.skip 1
            |> Seq.map (fun s -> s.Split '\t')
            |> Seq.map (fun a -> {date = a.[1]; sales = Decimal.Parse(a.[7]); currency = a.[8]});;

let values = salesRecord.fromFile "test.txt"
*)

let dir = new DirectoryInfo(@"C:\Users\Mdaugustine\Documents\adr-financials\adr-sales")
type SalesReport = CsvProvider<Sample = "test.txt", Separators="\t", InferRows=0, IgnoreErrors=true, HasHeaders=true>

let outFile = new StreamWriter("testing.txt")

let files = dir.GetFiles()

let currencies (report:SalesReport)= 
    report.Rows
    |> Seq.map (fun row -> row.``Customer Currency``)
    |> Seq.head

let sumSales (report:SalesReport) =
    report.Rows
    |> Seq.map (fun row -> row.``Extended Partner Share``)
    |> Seq.filter (fun elem -> not (Double.IsNaN (float(elem))))
    |> Seq.sum

let sales = 
    files
    |> Seq.map (fun f -> SalesReport.Load(f.FullName))
    |> Seq.map (fun report -> sumSales report)

let currency =
    files
    |> Seq.map (fun f -> SalesReport.Load(f.FullName))
    |> Seq.map (fun report -> currencies report)

(*
let sales =
    [for file in files do
        let salesReport = SalesReport.Load(file.FullName)
        //let sum = sumSales salesReport
        //outFile.WriteLine(sprintf "%f and %s" sum file.Name)
        yield salesReport]
    |> Seq.map (fun report -> sumSales report)
*)

for sale in sales do
    outFile.WriteLine(sprintf "%f" sale)

//for c in currency do
//        printfn "%s" c
//for currency in currencies do
//    outFile.WriteLine(sprintf "%s" currency.``Customer Currency``)


(*
let first (report:SalesReport) = 
    report.Rows
    |> Seq.map (fun row -> row.``Customer Currency``)
    |> Seq.head

let currencies =
    [for file in dir.GetFiles() do
        let salesReport = SalesReport.Load(file.FullName)
        yield salesReport]
        |> Seq.map (fun report -> first report)

for currency in currencies do
    printfn "%s" currency
let salesReport2 = SalesReport.Load("test.txt")
*)
//let first = salesReport2.Rows |> Seq.head
//let currency = first.``Customer Currency``

//printfn "%s" currency

//for sale in sales do
//    printfn "%f" sale

(*
let salesReport = SalesReport.Load("test.txt")

let totalSales = 
    salesReport.Rows
    |> Seq.map (fun row -> row.``Extended Partner Share``)
    |> Seq.sum

printfn "%f" totalSales
*)
(*
let values = 
    "test.txt"
    |> File.ReadAllLines
    |> Array.mapi (fun i line -> line.Split([|'\t'|]))

let rotate (arr:'a[][]) = 
    Array.map (fun y -> (Array.map (fun x -> arr.[x].[y])) [|0..arr.Length - 1|]) [|0..arr.[0].Length - 1|]

let newarrs = Array.map Array.sum (rotate values)

(*
let MultiArray (inp:String[][]) =
    let count = inp |> Array.length
    let rows = inp.[0] |> Array.length
    Array2D.init count rows (fun i j -> inp.[j].[i])

let newValues = MultiArray values

let flatten (A:'a[,]) = A |> Seq.cast<'a>

let getColumn c (A:_[,]) =
    flatten A.[*,c..c] |> Seq.toArray

let salesC = getColumn 7 newValues
*)
//for i in values do
//    printfn "%s" i.[7]
     
let date = 
  let month = Int32.Parse(values.[1].[1].Split([|'/'|]).[0]) - 1
  let year = Int32.Parse(values.[1].[1].Split([|'/'|]).[2])
  if month < 1 then (12, year - 1) else (month, year)

let sales = Decimal.Parse(values.[values.Length - 2].[1])
let currency = values.[1].[8]

Console.WriteLine("month " + date.ToString())
Console.WriteLine("sales " + sales.ToString())
Console.WriteLine("currency " + currency)

(*
for i in values do
  for j in i do
     printfn "%s" j
*)*)
[<EntryPoint>]
let main argv = 
    printfn "%A" argv
    0 // return an integer exit code