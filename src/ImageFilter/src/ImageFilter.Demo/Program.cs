using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

#pragma warning disable CA1416

// Load image
var imageFilePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "Images", "input.png");
var bytes = await Image.LoadAsync<Rgba32>(imageFilePath);

var width = bytes.Width;
var height = bytes.Height;

// Define filter kernel
var filterKernel = new[,] { { -1, -1, -1 }, { -1, 8, -1 }, { -1, -1, -1 } };
var kernelHeight = filterKernel.GetLength(1);
var kernelWidth = filterKernel.GetLength(0);

// Apply filter
var filteredImage = new Image<Rgba32>(width, height);
for (var imageHeightIndex = 0; imageHeightIndex < height - kernelHeight; imageHeightIndex++)
for (var imageWidthIndex = 0; imageWidthIndex < width - kernelWidth; imageWidthIndex++)
{
    var sum = 0L;

    for (var kernelHeightIndex = 0; kernelHeightIndex < kernelHeight; kernelHeightIndex++)
    for (var kernelWidthIndex = 0; kernelWidthIndex < kernelWidth; kernelWidthIndex++)
        sum += bytes[imageWidthIndex + kernelWidthIndex, imageHeightIndex + kernelHeightIndex].R * filterKernel[kernelHeightIndex, kernelWidthIndex];

    // Apply clapping to sum
    sum = Math.Max(Math.Min(sum, 255), 0);
    filteredImage[imageWidthIndex, imageHeightIndex] = new Rgba32(sum, sum, sum, 255);
}

// Write filtered image to file
await filteredImage.SaveAsPngAsync(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "Images", "actual-result.png"));