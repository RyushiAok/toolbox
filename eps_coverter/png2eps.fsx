#r "nuget: Aspose.Page"

open System.IO
open System.Text
open System.Diagnostics
open Aspose.Page.EPS
open Aspose.Page.EPS.Device

let saveAsEPS baseDir ourDir =
    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance)
    let options = PsSaveOptions()

    Directory.EnumerateFiles(baseDir, "*.png", SearchOption.AllDirectories)
    |> Seq.iter (fun filePath ->
        let relativePath =
            filePath.Replace(baseDir, "").TrimStart(Path.DirectorySeparatorChar)

        let outPath = Path.Combine(ourDir, relativePath).Replace(".png", ".eps")
        Directory.CreateDirectory(Path.GetDirectoryName(outPath)) |> ignore
        printfn "%s" outPath
        PsDocument.SaveImageAsEps(filePath, outPath, options))


let path = fsi.CommandLineArgs[1]
let ourDir = Path.Combine(path, "out_eps")
saveAsEPS path ourDir
