﻿namespace CampusHub.Services.Settings;

public class MainSettings
{
    public string AllowedOrigins { get; private set; }
    public string PublicUrl { get; private set; }
    public string StudentFrontUrl { get; private set; }
    public string AdminFrontUrl { get; private set; }
}