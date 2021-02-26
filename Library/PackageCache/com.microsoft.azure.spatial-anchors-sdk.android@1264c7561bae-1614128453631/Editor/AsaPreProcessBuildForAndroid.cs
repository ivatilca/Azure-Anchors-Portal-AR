using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor.Android;

public class AsaPreProcessBuildForAndroid : IPostGenerateGradleAndroidProject
{
    // Required dependencies, and minimum version numbers
    private readonly Dictionary<string, string> requiredDependencies = new Dictionary<string, string>
    {
        { "com.squareup.okhttp3:okhttp", "3.11.0"},
        { "com.microsoft.appcenter:appcenter-analytics", "1.10.0"}
    };

    // Callbacks with lower values are called before ones with higher values
    public int callbackOrder => 0;

    // Called after the Android Gradle project is generated, and before building begins
    public void OnPostGenerateGradleAndroidProject(string path)
    {
        string generatedGradlePath = Path.Combine(path, "build.gradle");

        // ReadAllText to return a single string
        string gradleContents = File.ReadAllText(generatedGradlePath);

        // Check if the build.gradle contains the dependencies and their versions
        foreach (KeyValuePair<string, string> dependency in requiredDependencies)
        {
            if (!gradleContents.Contains(dependency.Key))
            {
                // Ensure the build fails if required dependencies don't exist
                throw new UnityEditor.Build.BuildFailedException($"Missing dependency: {dependency.Key}\n"
                    + "Please verify that a custom mainTemplate.gradle exists and that this dependency is included.\n"
                    + "For more details, see: https://docs.unity3d.com/Manual/android-gradle-overview.html");
            }

            // Regex pattern: matches to the version of the given key
            string dependencyVersionPattern = @"(?<=" + Regex.Escape(dependency.Key) + @"\:\[)([^\]]*)";

            Match getIncludedVersion = Regex.Match(gradleContents, dependencyVersionPattern);

            Version includedVersion = Version.Parse(getIncludedVersion.Value);
            Version requiredVersion = Version.Parse(dependency.Value);

            if (includedVersion.CompareTo(requiredVersion) < 0)
            {
                // If the included version is less than the minimum version, throw an error
                throw new UnityEditor.Build.BuildFailedException($"Minimum version of {dependency.Value} is required for {dependency.Key}.\n"
                    + $"Included version is {includedVersion}. Please update the version in your mainTemplate.gradle.");
            }
        }
    }
}
