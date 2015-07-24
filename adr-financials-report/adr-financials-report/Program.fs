// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.
open System.IO
open System

let filePath = "test.txt"

let readLines = seq {
    use sr = new StreamReader (filePath)
    while not sr.EndOfStream do
        //for tab in sr.ReadLine().Split([|'\t'|]) do
        yield sr.ReadLine().Split([|'\t'|])
}

readLines |> Seq.iter(fun x -> x |> Seq.iter(fun y -> printfn "%s" y))

[<EntryPoint>]
let main argv = 
    printfn "%A" argv
    0 // return an integer exit code