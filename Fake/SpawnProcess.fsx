open System.Diagnostics
open System.IO

let spawnProcess (processName:string, arguments:string) =
    let startInfo = new ProcessStartInfo(processName)
    startInfo.Arguments <- arguments
    startInfo.RedirectStandardInput <- true
    startInfo.RedirectStandardOutput <- true
    startInfo.RedirectStandardError <- true
    startInfo.UseShellExecute <- false
    startInfo.CreateNoWindow <- true

    use proc = new Process(StartInfo = startInfo)
    proc.Start() |> ignore

    let reader = new StreamReader(proc.StandardOutput.BaseStream, System.Text.Encoding.UTF8)
    let result = reader.ReadToEnd()
    proc.WaitForExit()
    if proc.ExitCode <> 0 then 
        failwith ("Problems spawning ("+processName+") with arguments ("+arguments+"): \r\n" +  proc.StandardError.ReadToEnd())

    result
