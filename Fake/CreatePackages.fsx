#load "Globals.fsx"

open Fake
open Fake.DotNetCli
open Globals

Target "CreatePackages" (fun _ ->
    trace "**** CreatePackages ****"

    let projects = !! "./*.sln"

    let buildProject project =
        tracef "Packing : %s" project
        DotNetCli.Pack
            (fun p ->
                { p with
                    Project = project
                    Configuration = "Release"
                    AdditionalArgs = ["--no-restore"; "--no-build";"--include-symbols";"--include-source"]
                    OutputPath = Globals.NuGetOutputPath })

    projects |> Seq.iter (buildProject)

    trace "**** CreatePackages DONE ****"
)