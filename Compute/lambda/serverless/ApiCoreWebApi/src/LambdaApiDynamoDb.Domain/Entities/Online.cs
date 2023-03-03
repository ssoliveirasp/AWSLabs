﻿using System.Reflection;

namespace lambdaApi.Domain.Entities;
public class OnlineService
{
    public static string Works => $"works Domain " +
                                  $"Version 1.3.Application: {Assembly.GetExecutingAssembly().GetName().Version} " +
                                  $"Date:{DateTime.UtcNow}";
}