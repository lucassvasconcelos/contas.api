#addin nuget:?package=Cake.Coverlet&version=2.5.4
#tool dotnet:?package=dotnet-reportgenerator-globaltool&version=5.0.2
var target = Argument("target", "IntegrationTests");
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

Task("UnitTests").IsDependentOn("Build").Does(() => {
    var coverletSettings = new CoverletSettings {
        CollectCoverage = true,
        CoverletOutputFormat = CoverletOutputFormat.cobertura,
        CoverletOutputDirectory = Directory("./tests/.coverage"),
        CoverletOutputName = "unit-cov",
        ThresholdType = ThresholdType.Line | ThresholdType.Branch,
        Threshold = 100
    };

    var testSettings = new DotNetCoreTestSettings { Configuration = configuration, NoBuild = true };
    DotNetCoreTest(unitTestsProject, testSettings, coverletSettings);
}).Finally(() => {
    ReportGenerator(
        report: "./tests/.coverage/unit-cov.cobertura.xml",
        $"./coveragereport/unit-tests",
        new ReportGeneratorSettings { ArgumentCustomization = args => args.Append("-reporttypes.Html")}
    );
});

Task("IntegrationTests").IsDependentOn("UnitTests").Does(() => {
    var coverletSettings = new CoverletSettings {
        CollectCoverage = true,
        CoverletOutputFormat = CoverletOutputFormat.cobertura,
        CoverletOutputDirectory = Directory("./tests/.coverage"),
        CoverletOutputName = "intg-cov",
        ThresholdType = ThresholdType.Line | ThresholdType.Branch,
        Threshold = 100
    };

    var testSettings = new DotNetCoreTestSettings { Configuration = configuration, NoBuild = true };
    DotNetCoreTest(integrationTestsProject, testSettings, coverletSettings);
}).Finally(() => {
    ReportGenerator(
        report: "./tests/.coverage/intg-cov.cobertura.xml",
        $"./coveragereport/intg-tests",
        new ReportGeneratorSettings { ArgumentCustomization = args => args.Append("-reporttypes.Html")}
    );
});

RunTarget(target);