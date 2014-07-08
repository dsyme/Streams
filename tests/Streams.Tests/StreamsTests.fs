﻿namespace Nessos.Streams.Tests
    open System.Linq
    open FsCheck
    open FsCheck.Fluent
    open NUnit.Framework
    open Nessos.Streams.Core

    [<TestFixture>]
    type ``Streams tests`` () =

        [<Test>]
        member __.``map`` () =
            Spec.ForAny<int[]>(fun xs ->
                let x = xs |> Stream.ofArray |> Stream.map (fun n -> 2 * n) |> Stream.toArray
                let y = xs |> Seq.map (fun n -> 2 * n) |> Seq.toArray
                x = y).QuickCheckThrowOnFailure()

        [<Test>]
        member __.``filter`` () =
            Spec.ForAny<int[]>(fun xs ->
                let x = xs |> Stream.ofArray |> Stream.filter (fun n -> n % 2 = 0) |> Stream.toArray
                let y = xs |> Seq.filter (fun n -> n % 2 = 0) |> Seq.toArray
                x = y).QuickCheckThrowOnFailure()

        [<Test>]
        member __.``collect`` () =
            Spec.ForAny<int[]>(fun xs ->
                let x = xs |> Stream.ofArray |> Stream.collect (fun n -> [|1..n|] |> Stream.ofArray) |> Stream.toArray
                let y = xs |> Seq.collect (fun n -> [|1..n|]) |> Seq.toArray
                x = y).QuickCheckThrowOnFailure()
            

        [<Test>]
        member __.``sum`` () =
            Spec.ForAny<int[]>(fun xs ->
                let x = xs |> Stream.ofArray |> Stream.map (fun n -> 2 * n) |> Stream.sum
                let y = xs |> Seq.map (fun n -> 2 * n) |> Seq.sum
                x = y).QuickCheckThrowOnFailure()

        [<Test>]
        member __.``length`` () =
            Spec.ForAny<int[]>(fun xs ->
                let x = xs |> Stream.ofArray |> Stream.filter (fun n -> n % 2 = 0) |> Stream.length
                let y = xs |> Seq.filter (fun n -> n % 2 = 0) |> Seq.length
                x = y).QuickCheckThrowOnFailure()

        [<Test>]
        member __.``sortBy`` () =
            Spec.ForAny<int[]>(fun xs ->
                let x = xs |> Stream.ofArray |> Stream.map ((+) 1) |> Stream.sortBy id
                let y = xs |> Seq.map ((+) 1) |> Seq.sortBy id |> Seq.toArray
                x = y).QuickCheckThrowOnFailure()

        [<Test>]
        member __.``groupBy`` () =
            Spec.ForAny<int[]>(fun xs ->
                let x = xs 
                        |> Stream.ofArray 
                        |> Stream.groupBy id 
                        |> Stream.ofSeq 
                        |> Stream.map (fun (key, values) -> (key, values |> Seq.length))
                        |> Stream.toArray
                let y = xs  
                        |> Seq.groupBy id 
                        |> Seq.map (fun (key, values) -> (key, values |> Seq.length))
                        |> Seq.toArray
                x = y).QuickCheckThrowOnFailure()