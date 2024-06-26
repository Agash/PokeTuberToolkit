﻿using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;

namespace PokeTuberToolkit.OBS.WidgetsServer;

internal class WidgetServerConfigureOptions(IWebHostEnvironment environment) : IPostConfigureOptions<StaticFileOptions>
{
    private readonly IWebHostEnvironment _environment = environment;

    public void PostConfigure(string? name, StaticFileOptions options)
    {

        // Basic initialization in case the options weren't initialized by any other component
        options.ContentTypeProvider ??= new FileExtensionContentTypeProvider();

        if (options.FileProvider == null && _environment.WebRootFileProvider == null)
        {
            throw new InvalidOperationException("Missing FileProvider.");
        }

        options.FileProvider ??= _environment.WebRootFileProvider;


        // Add our provider
        var filesProvider = new ManifestEmbeddedFileProvider(GetType().Assembly, "wwwroot");
        options.FileProvider = new CompositeFileProvider(options.FileProvider, filesProvider);
    }
}