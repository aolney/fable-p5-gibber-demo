module FableP5GibberDemo

open Fable.Core
open Fable.Import
open Fable.Core.JsInterop

//Using p5.gibber.js with foreign interfaces weird b/c it updates the p5 prototype with gibber functions
//we can't just add these functions to the p5 definition b/c then gibber is never imported, but even if we import 
//gibber we can't put the functions under p5. Seems more parsimonious to keep gibber separate, especially 
//since p5.gibber.js is a very thin wrapper around gibber anyways. However gibber.audio.lib doesn't work with 
//dts-gen

//let p5Import : obj = importAll  "p5.gibber.js"

let sketch  = 
    new System.Func<obj,unit>(
        fun o ->
            let mutable drums = null
            let x = 100.0;
            let y = 100.0;
            let p = o |> unbox<p5>
            p.setup <- fun() -> 
                p.createCanvas(700.0, 410.0) |> ignore
                drums <- p.EDrums("x*o*x*o-")                
                ()
            p.draw <- fun() ->
                
                if p.mouseIsPressed |> unbox<bool> then
                    p.fill(0.0 |> U4.Case1)
                else
                    p.fill(255.0 |> U4.Case1)
                p.rect(p.mouseX |> unbox<float>, p.mouseY |> unbox<float>, 50.0,50.0)
                |> ignore
            ()
    )

let myp5 = p5(sketch);
