#r "nuget: QuestPDF"
#r "nuget: System.Drawing.Common"

open System.IO
open System.Text
open QuestPDF.Fluent

let img2pdf (src: string) (dist: string) =
    let image = System.Drawing.Image.FromFile(src)

    let doc =
        Document.Create(fun container ->
            container.Page(fun page ->
                page.Margin(0f)
                page.ContinuousSize(float32 image.Width)
                page.Content().Image(src))
            |> ignore)

    doc.GeneratePdf(dist)



let saveAsPDF baseDir ourDir =
    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance)

    Directory.EnumerateFiles(baseDir, "*.png", SearchOption.AllDirectories)
    |> Seq.iter (fun filePath ->
        let relativePath =
            filePath.Replace(baseDir, "").TrimStart(Path.DirectorySeparatorChar)

        let outPath = Path.Combine(ourDir, relativePath).Replace(".png", ".pdf")
        Directory.CreateDirectory(Path.GetDirectoryName(outPath)) |> ignore

        img2pdf filePath outPath

        printfn "%s" outPath)


let path = fsi.CommandLineArgs[1]
let ourDir = Path.Combine(path, "out_pdf")
saveAsPDF path ourDir
