#addin nuget:?package=Cake.Coverlet&version=2.5.4
var target = Argument("target", "IntegrationTest");
var configuration = Argument("configuration", "Release");
var solution = "./Contas.API.sln";
var unitTestsProject = "./tests/Contas.UnitTests";
var integrationTestsProject = "./tests/Contas.IntegrationTests";

Task("Clean").Does(() => {
    CleanDirectories($"./src/**/bin/{configuration}");
    CleanDirectories($"./tests/**/bin/{configuration}");
});

Task("Restore").IsDependentOn("Clean").Does(() => {
    DotNetCoreRestore(solution, new DotNetCoreRestoreSettings { NoCache = true });
});

Task("Build").IsDependentOn("Restore").Does(() => {
    DotNetCoreBuild(solution, new DotNetCoreBuildSettings { Configuration = configuration });
});

Task("UnitTest").IsDependentOn("Build").Does(() => {
    var coverletSettings = new CoverletSettings
    {
        CollectCoverage = true,
        CoverletOutputFormat = CoverletOutputFormat.cobertura,
        CoverletOutputDirectory = Directory("./tests/.coverage"),
        CoverletOutputName = "cov",
        ThresholdType = ThresholdType.Line | ThresholdType.Branch,
        Threshold = 100
    };

    var testSettings = new DotNetCoreTestSettings { Configuration = configuration, NoBuild = true };
    DotNetCoreTest(unitTestsProject, testSettings, coverletSettings);
});

Task("IntegrationTest").IsDependentOn("UnitTest").Does(() => {
    var coverletSettings = new CoverletSettings
    {
        CollectCoverage = true,
        CoverletOutputFormat = CoverletOutputFormat.cobertura,
        CoverletOutputDirectory = Directory("./tests/Contas.IntegrationTests/.coverage"),
        CoverletOutputName = "cov",
        ThresholdType = ThresholdType.Line | ThresholdType.Branch,
        Threshold = 100
    };

    var testSettings = new DotNetCoreTestSettings { Configuration = configuration, NoBuild = true };
    DotNetCoreTest(integrationTestsProject, testSettings, coverletSettings);
});

RunTarget(target);