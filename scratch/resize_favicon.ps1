Add-Type -AssemblyName System.Drawing
$src = "public\favicon.png"
$dest = "public\favicon-48.png"
$img = [System.Drawing.Image]::FromFile((Resolve-Path $src))
$newImg = New-Object System.Drawing.Bitmap(48, 48)
$g = [System.Drawing.Graphics]::FromImage($newImg)
$g.InterpolationMode = [System.Drawing.Drawing2D.InterpolationMode]::HighQualityBicubic
$g.DrawImage($img, 0, 0, 48, 48)
$newImg.Save((Join-Path (Get-Location) $dest), [System.Drawing.Imaging.ImageFormat]::Png)
$g.Dispose()
$newImg.Dispose()
$img.Dispose()
