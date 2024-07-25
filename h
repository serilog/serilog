[33mcommit 2eb1fd5224af087c3836d7244dc7167f0b2b1e32[m[33m ([m[1;36mHEAD -> [m[1;32mreuse-hashsets[m[33m)[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Jul 23 14:01:26 2024 +1000

    Optimize structure capturing to reduce allocations

[33mcommit 883efd695df208fcd7cfe621ab24e8dbf8026639[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Jul 23 14:00:50 2024 +1000

    Add a binding benchmark for structure capturing.

[33mcommit d98f7fa2cf62dc11bea74a0791797f2b406e42f2[m[33m ([m[1;31mupstream/dev[m[33m, [m[1;31morigin/dev[m[33m, [m[1;31morigin/HEAD[m[33m, [m[1;32mdev[m[33m)[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Jul 12 07:16:16 2024 +1000

    Version Serilog.dll as Major.Minor.0.0 (#2083)

[33mcommit fdf4a4872b4effd02dcd7d788e45b6b8ffdfa2fc[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Jun 1 16:32:05 2024 +1000

    Dev version bump [skip ci]

[33mcommit e96347593ee6218e8d34c698a90ab6ff7555a394[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu May 30 10:44:47 2024 +1000

    Attempt to make tests more robust on slow CI machines by increasing time steps

[33mcommit 5e18faff0392a8e17367582f18db2a6888c608b8[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon May 27 15:42:07 2024 +1000

    Update all dependencies, fix new xUnit lints

[33mcommit 9fcf795c9a9d7acf4d819a90d90c63ab06a7e80b[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon May 27 15:25:36 2024 +1000

    Move TrimConfiguration to Capturing namespace, 'Configuration' namespace is just for the public builder syntax

[33mcommit 4ef3a8772c2dee065a5c6d5ac951f405b6c7ced4[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon May 27 15:13:55 2024 +1000

    Remove noop byte cast

[33mcommit 9d3a088993109a3dd638498649a3add6d9e1d411[m
Author: Chris Nantau <Chris.Nantau@Hey.com>
Date:   Sat May 25 11:30:34 2024 -0300

    fix: only disallows '}' in format strings, use byte comparison

[33mcommit 920b8c87eff7ae6ae19ea48b6cc2168da0976017[m
Author: Chris Nantau <Chris.Nantau@Hey.com>
Date:   Thu May 23 22:09:01 2024 -0300

    fix: allows all printable characters as format values
    
    The existing implementation used .IsPunctuation + IsLetterOrDigit with
    an override for '+' and ' '. That left some conspicuous holes and the
    spec itself is just "^\}" so basically anything goes in a format string.
    This allows for them and this does seem to have a reasonably large
    impact on the DefaultConsoleOutputTemplate parsing method. Everything
    else was within error bounds and no impact at all on memory allocs.

[33mcommit e1a00477a487010aafb2cdfbac82426147e1a305[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon May 20 15:41:14 2024 +1000

    Typo - to -> do

[33mcommit 921eccc03bfbdd84d7c1ed748d47094c5ad267e4[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon May 20 09:28:26 2024 +1000

    Scope back to only considering dots; improve tests

[33mcommit 592a1245ff325e5c90962d13154c1a43a6685ad6[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sat May 18 07:36:53 2024 +1000

    Improve comment

[33mcommit c8d5cd84fe48e52b6865680ca1352e234c491af8[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sat May 18 06:53:37 2024 +1000

    Improve message template parsing benchmark

[33mcommit 2d2e614253c59865bf767c4a6b22ea1f85b3af9f[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri May 17 20:50:07 2024 +1000

    Use IndexOf() to improve property token parsing speed, too; modifies behavior of some invalid templates

[33mcommit fa723deb47cd22de43bf2298344092bdf822216a[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri May 17 20:44:48 2024 +1000

    Improve text token parsing - use IndexOfAny, stop allocating StringBuilders

[33mcommit d0a033b87db54485d0ff42da889304cbcef52389[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri May 17 19:28:47 2024 +1000

    Simplest possible change to allow dotted and dashed property names

[33mcommit 2d3e208951db13735400fedb53a98a4ff783ef28[m
Author: Chris Nantau <chris.nantau@hey.com>
Date:   Mon May 20 11:50:01 2024 -0300

    chore: early return instead of a parse and checking for sign chars

[33mcommit 3a2363671d777413b1798bfbba9321fdf4f5dcc3[m
Author: Chris Nantau <chris.nantau@hey.com>
Date:   Mon May 20 11:29:12 2024 -0300

    fix: adds check and test for + alignment to ensure it's parsed as text

[33mcommit eaf2ba38b3b0b3d851f909b263596fd1a309a257[m
Author: Chris Nantau <chris.nantau@hey.com>
Date:   Mon May 20 11:12:23 2024 -0300

    chore(cleanup): changes alignment to a simple int parse and '-' check

[33mcommit 16db14379324937ceb6f79281b48f2f203bc537d[m
Author: Chris Nantau <chris.nantau@hey.com>
Date:   Sun May 19 13:32:03 2024 -0300

    chore(cleanup): remove unused IsValidInAlignment func

[33mcommit e5a7ac0d224e3a8a6129dc5450ccdbb89cdbbb90[m
Author: Chris Nantau <chris.nantau@hey.com>
Date:   Sun May 19 13:15:07 2024 -0300

    chore(tests): changes zero value alignment test to match messagetemplates sepec

[33mcommit c74854689368e383c39fb2013285614ff667b46a[m
Author: Chris Nantau <Chris.Nantau@Hey.com>
Date:   Sat May 18 13:59:59 2024 -0300

    fix: message-template spec allows for zero alignments

[33mcommit e28690fa779f6cf6a437d260d5cab25eca683e1b[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun May 12 07:09:45 2024 +1000

    Update test/Serilog.Tests/Configuration/LoggerSinkConfigurationTests.cs
    
    Co-authored-by: Ruben Bartelink <ruben@bartelink.com>

[33mcommit 614c42f909d676b549e9ba86393ff385b4d60705[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun May 12 07:09:28 2024 +1000

    Update src/Serilog/Configuration/LoggerSinkConfiguration.cs
    
    Co-authored-by: Ruben Bartelink <ruben@bartelink.com>

[33mcommit 5aef0fa02563a20bb08ca737d02743a6fe36b0aa[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun May 12 07:09:11 2024 +1000

    Update test/Serilog.Tests/Core/AuditSinkTests.cs
    
    Co-authored-by: Ruben Bartelink <ruben@bartelink.com>

[33mcommit acb0176c1d70318ed40786ef85e498065d141ef1[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sat May 11 08:36:24 2024 +1000

    Remove another duplicate test

[33mcommit c2683486e174b5f868c2b70f8234be4f7f5b0907[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sat May 11 08:13:48 2024 +1000

    Redesign LoggerSinkConfiguration.Wrap(), and introduce simpler .CreateSink() method

[33mcommit 73325f64a1e4192751c3c067a300c1f541a7d443[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue May 7 17:33:52 2024 +1000

    Ditch attempt to tidy up using ManualResetEvent; made test flaky on Ubuntu

[33mcommit 2bc3f3ac5978ff983f5f312d2c2138d796f4e7c4[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue May 7 15:01:07 2024 +1000

    Fix the namespace of the new BatchingOptions type

[33mcommit 1660691b133465c60d63ab5921004a082c3b1e17[m
Author: Andy Gocke <andy@commentout.net>
Date:   Sun Mar 17 20:47:46 2024 -0700

    Update baseline

[33mcommit 9b4d6cc0781eceafdd8ec8ad5ec10f5f43642a40[m
Author: Andy Gocke <andy@commentout.net>
Date:   Sun Mar 17 19:55:05 2024 -0700

    Fix AOT problems and introduce feature switch
    
    There's one trim warning that the analyzer didn't catch (bug) that the
    AOT compiler does. Unfortunately, the warning isn't easily resolvable --
    it implies that you can no longer use deconstructed compiler-generated
    types (like anonymous types) through the various logging calls.
    
    There's unfortunately no way to perfectly resolve this. This PR proposes
    a behavioral change for trimming -- when trimming, compiler-generated
    types will be logged like scalars, instead of being deconstructed. This
    is not ideal. Libraries should not have significantly different behavior
    when trimming without a warning or some other diagnostic being produced.
    
    Unfortunately, the contract implied by the existing code is much too
    complicated to be encoded in the trimmer. The contract is that types do
    not need to preserve reflection metadata, except for compiler-generated
    types, which need public properties. However, the definition of
    compiler-generated types is not well-defined. Encoding a heuristic in
    Serilog is one thing -- encoding the heuristic in the .NET runtime is
    another.
    
    Given the constraints, this seems like the best option.

[33mcommit 590855b427c0edb8273e6863b24577b3e3351083[m
Author: Andy Gocke <andy@commentout.net>
Date:   Fri Nov 24 16:14:00 2023 -0800

    Add test app for testing trimming and AOT

[33mcommit 834f1d71fb2cfbbdf885dffdbe7d173dbbecef8e[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu May 2 10:26:38 2024 +1000

    Don't try capturing SelfLog through ITestOutputHelper

[33mcommit d9541bd9ad7255c366d6ad324acd5b661656a25b[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu May 2 09:53:22 2024 +1000

    WriteTo.Sink(IBatchedLogEventSink, BatchingOptions, ...)

[33mcommit cb06fb366812828211823ee9895844d93c1fd8f7[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Apr 30 16:53:05 2024 +1000

    Collection expressions

[33mcommit a7eb5a75640319a0791d2e3afc3497fbe7edcf7a[m
Author: MatthewHays <Matthew.Hayhurst@gmail.com>
Date:   Wed Apr 24 00:58:33 2024 +0100

    Add a UtcTimestamp token and a small unit test (#2051)
    
    * Add the UTCTimestamp token and a small unit test
    
    * Use the Some.LogeEvent pattern to create the logevent
    
    * Remove unnecessary system include
    
    * Fix PublicApi_Should_Not_Change_Unintentionally test
    
    * Change UTCTimestamp  to UtcTimestamp after MR feedback
    
    * Rename test method to Utc as well
    
    * Chnage call to UtcDateTime

[33mcommit 56a26bd61cae72a8f919c6d55e61fcbb703a11c7[m
Author: Travis Illig <tillig@paraesthesia.com>
Date:   Tue Apr 23 16:58:02 2024 -0700

    Level override check is case-insensitive. (#2045)
    
    * Level override check is case-insensitive.
    
    * Swapped context evaluation functions to fail faster.
    
    * Corrected length check.

[33mcommit f5383bcdc011ef612bfbc2f666dfd85d11a2d752[m
Author: Travis Illig <tillig@paraesthesia.com>
Date:   Thu Apr 4 16:04:29 2024 -0700

    Build updates for compatibility (#2044)
    
    * Allow branches with slashes.
    
    * Pop-Location after Powershell build.
    
    * Enable default VS Code build and test support.
    
    * VS Code uses the first TFM listed for Intellisense, etc.
    
    * Performance test project TFMs must include SimpleJob TFMs.
    
    While this isn't documented as strictly necessary, a problem arises where libraries or runtimes aren't available for the auto-generated BenchmarkDotNet assemblies, which results in warnings and errors failing the benchmarks.
    
    By adding the TFMs to the benchmark assembly, we'll get build errors/warnings up front about things that won't work.
    
    These were also upgraded because libraries like System.Diagnostics.DiagnosticSource throw warnings about not being supported or tested on older platforms like netcoreapp3.1.
    
    * Exclude benchmarks for default test command in VS Code.

[33mcommit 4bd40f1241057ebeed2062811d4990236be48ba5[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Mar 12 09:18:33 2024 +1000

    Add LogEvent.UnstableAssembleFromParts(); fixes #2010 (#2018)

[33mcommit 88782d0f506bf582f3fbb42dc542b4365b9bf0fc[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Mar 1 13:48:57 2024 +1000

    Update TFMs for Serilog 4.0 (#2016)
    
    * Update TFMs for #1970
    
    * 'Policy' wording - review feedback
    
    * Update src/Serilog/Serilog.csproj
    
    Co-authored-by: Ruben Bartelink <ruben@bartelink.com>
    
    ---------
    
    Co-authored-by: Ruben Bartelink <ruben@bartelink.com>

[33mcommit df8b84f44f244abc992ba791e78bd4fea647cc0e[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Feb 26 08:00:14 2024 +1000

    Set dev branch version to `4.0.0-*`, unpin `[AssemblyVersion]` (#2015)
    
    * Set dev branch version to 4.0.0-*, unpin [AssemblyVersion]
    
    * .NET 8 SDK sets deterministic source paths by default

[33mcommit 653260089620944516cbd874449f3c4bd02edc0e[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Nov 17 18:12:17 2023 +1100

    Add tests for ReusableStringWriter not covered on other paths (#1979)

[33mcommit a46064f636c52f5e365f8b41c7bd18ebf144ae06[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Nov 10 22:40:13 2023 +1000

    Dev version bump [skip ci]

[33mcommit 16739f0f81f0800d52365a20cc573dd18333e9eb[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Nov 10 23:19:34 2023 +1100

    Don't stack overflow when disposing `ReusableStringWriter` (#1977)
    
    * Don't stack overflow when disposing ReusableStringWriter
    
    * Don't attempt to reuse a pooled writer if it's being finalized

[33mcommit ca4efda2b06106d3843396dd0acc3f3a3c489fb3[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Nov 9 16:54:46 2023 +1000

    Dev version bump [skip ci]

[33mcommit e37837ebf5d819ce4cd6702c43154c89a4b764f0[m
Author: Ruben Bartelink <ruben@bartelink.com>
Date:   Fri Oct 27 00:45:31 2023 +0100

    chore(docs): Markdown housekeeping (#1969)
    
    * Remove gitter from CONTRIBUTING.md
    * Remove year from copyright
    * Add Copyright prop for package
    * CloseAndFlushAsync comment
    * More shepherding to SO

[33mcommit a493ffdda2c62528223456ad37f2ac409ae12c6a[m
Author: Ruben Bartelink <ruben@bartelink.com>
Date:   Sun Oct 22 23:49:01 2023 +0100

    chore: Drop test coverage for out of support .NET Core vers (#1971)
    
    * Trim dead PropertyGroup
    
    * Trim test TFMs

[33mcommit e059e9fb36d4a38b95ffb120df2898cc8db8e797[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Oct 11 08:52:57 2023 +1000

    Make `StringBuilderCapacityThreshold` the even power of two that was intended

[33mcommit 8d0e2ed9ec4570d3273492c450ef9020311eb93f[m
Author: dtabakaev <116259451+Jakimar@users.noreply.github.com>
Date:   Tue Oct 10 01:38:38 2023 +0300

    ReusableStringWriter: Dispose instance with too big buffer (#1964)

[33mcommit 88f76a872f8b5f6ae5302b7002d6c72577578ea1[m
Author: Evgeny Peshkov <epeshk@users.noreply.github.com>
Date:   Mon Oct 9 10:05:13 2023 +0300

    By reference string comparison in template cache (#1947)
    
    * By reference string comparison in template cache
    
    * Removed by-value template cache

[33mcommit d6e80e6682d115241ca20893178284573085b2de[m
Author: Evgeny Peshkov <epeshk@users.noreply.github.com>
Date:   Wed Oct 4 01:30:51 2023 +0300

    Removed temporary array allocations for properties (#1948)
    
    * Removed temporary array allocations for cases with the small number of log event properties
    
    * Added test to ensure that null array does not break message template parameters binding
    
    * Excluded ILogger.Write<T1, ...> methods from method body comparison test.

[33mcommit 948ea83cbd83d386af5f2afead92729ae216196e[m
Author: Evgeny Peshkov <epeshk@users.noreply.github.com>
Date:   Tue Oct 3 05:42:40 2023 +0300

    Added missing DynamicallyAccessedMembers in PropertyValueConverter to fix error when building without FEATURE_ITUPLE (#1960)

[33mcommit 9b658c1b6be419fbbe78854aa16955a5ccbdc6d8[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Oct 2 15:38:23 2023 +1000

    Collect and propagate current trace and span ids through `LogEvent` (#1955)

[33mcommit db7900d74e742bd684ae00ddee03011738a67fa7[m
Author: Evgeny Peshkov <epeshk@users.noreply.github.com>
Date:   Mon Oct 2 07:55:20 2023 +0300

    PrimitiveScalarConversionPolicy: avoiding hashset lookup for common types (#1959)

[33mcommit 1ce8f9110ca22307eb2134be27845a90079878bc[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Sep 20 11:07:53 2023 +1000

    Update NuGet publishing key

[33mcommit 63bff1e93f8141b1ffa9f1632d05f90c72c42f65[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Sep 13 14:34:40 2023 +1000

    Modernize the README a little more (#1922)
    
    * Modernize the README a little more
    
    * Improve example
    
    Co-authored-by: C. Augusto Proiete <augusto@proiete.com>
    
    ---------
    
    Co-authored-by: C. Augusto Proiete <augusto@proiete.com>

[33mcommit 801898d43e751632315f42d2082f767ef6a888da[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Tue Aug 1 21:27:47 2023 +1000

    remove some redundant GetTypeInfo (#1942)

[33mcommit a28a392e96fd35521f7998fdf2d77994d39a7db0[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Jul 10 14:04:11 2023 +1000

    Update upgrading instructions in README.md (#1936)

[33mcommit fd1e8eecac276d19ae3394c8b660d30893bdbe4e[m
Author: Ivan Maximov <sungam3r@yandex.ru>
Date:   Thu Jul 6 19:27:56 2023 +0300

    Remove Changes.md (#1935)

[33mcommit 3016aa31772f882d294ed356d13207d6f55901c0[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Jun 22 07:56:51 2023 +1000

    dev version bump [skip ci]

[33mcommit cf2c62e3565c3c397e2c046cfbf42c7a146ab331[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Jun 22 07:21:54 2023 +1000

    Fixes #1924 - JsonFormatter produces malformed JSON when renderMessage = true (#1926)

[33mcommit 2424cae597d19b12ea0b5e0b033a74810dc8bbbe[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Jun 20 09:08:53 2023 +1000

    Dev version bump [skip ci]

[33mcommit b8da8504f268224449828f6e3bf61277a94169d5[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Jun 19 15:15:36 2023 +1000

    Serve README.md logo from raw.github.com so that it will be displayed on NuGet.org (#1919)

[33mcommit 0f7affa49847116ba7a1c4e54c892123c121b1b5[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Jun 19 14:02:31 2023 +1000

    Include `README.md` in the NuGet package for display on nuget.org (#1916)
    
    * Include README in NUPKG for display on nuget.org
    
    * Gitter chat is no longer in use; test count shield is inaccurate/misleading

[33mcommit 826cb10febe7f7211228482f76d1548207d936bd[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed May 31 11:59:01 2023 +1000

    Use `JsonValueFormatter` to implement classic `JsonFormatter` (#1911)
    
    Since 3.0 removes all the obsolte JsonFormatter virtual methods, we can now seal this and remove the duplicated functionality that existed solely for subclass use

[33mcommit 3208c64d57a3aa59252b3e581c28e32187f14767[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon May 29 10:34:30 2023 +1000

    Add `LevelAlias.Off`; fixes #1684 (#1910)

[33mcommit 4610da89b438805bda0c42654c2f0b0499af2672[m
Author: Ivan Maximov <sungam3r@yandex.ru>
Date:   Thu May 25 00:08:40 2023 +0300

    Add Destructure.AsDictionary<T>() (#1906)

[33mcommit 5c7d2248a3a1f24f870e9666551ff020a5525314[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed May 24 14:25:50 2023 +1000

    Fix #1464, don't log parameter count mismatch message incorrectly (#1903)

[33mcommit f89303b0f43c159ace6a9368e671f8124ca10fc3[m
Author: Ivan Maximov <sungam3r@yandex.ru>
Date:   Wed May 24 07:24:34 2023 +0300

    Add LoggingLevelSwitch.MinimumLevelChanged (#1908)

[33mcommit 2994fabb55549d4a681f573c5a8196b6b15d882d[m
Author: Ivan Maximov <sungam3r@yandex.ru>
Date:   Tue May 9 09:58:57 2023 +0300

    Annotate WithProperty (#1907)

[33mcommit 64c25ef44b1937789f8a0dc7522b7fceb887da5a[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Sat May 6 08:04:40 2023 +1000

    add net47 (#1905)
    
    * add net47
    
    * Remove System.ValueTuple dependency from .NET 7 targets
    
    ---------
    
    Co-authored-by: Nicholas Blumhardt <nblumhardt@nblumhardt.com>

[33mcommit 7a38acdf033701bc6f49b68ad98060cf873de3c5[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed May 3 13:07:16 2023 +1000

    Accept/pass through the standard `levelSwitch` option in `WriteTo.Logger()` (#1902)
    
    * Accept/pass through the standard levelSwitch option when configuring SecondaryLoggerSink via WriteTo.Logger()
    
    * Update public API approval tests

[33mcommit 5291b4b8100f0acc1b9269b0394788a03979c714[m
Author: Ivan Maximov <sungam3r@yandex.ru>
Date:   Wed May 3 04:32:53 2023 +0300

    Optimize AddPropertyIfAbsent (#1872)
    
    * Optimize AddPropertyIfAbsent
    
    * mem
    
    * to instance method
    
    * No longer need to expose the internal `Dictionary` property

[33mcommit 33de1c86d93e655298e6863f7720f5b6afc49833[m
Author: Sergei Rogovtcev <lair@hawke.ru>
Date:   Tue May 2 20:28:49 2023 -0400

    Adding ability to dispose nested loggers in WriteTo.Logger (#1890)
    
    * Closes serilog/serilog#1447: ability to dispose nested loggers
    
    * serilog/serilog#1447: more consistent public interface

[33mcommit 155da5b43796631d99117102569a1e001de483bd[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Tue May 2 20:37:05 2023 +1000

    remove redundant idictionary cast (#1900)

[33mcommit 09b25c48ade29f5b6f30f771dad765d24eb96a6b[m
Author: Ivan Maximov <sungam3r@yandex.ru>
Date:   Mon May 1 07:58:40 2023 +0300

    Destructure ReadOnlyDictionary as Dictionary (#1897)
    
    * Destructure ReadOnlyDictionary as Dictionary
    
    * is

[33mcommit 41e5f476f0c02915794a2b31078c3d47e822d42d[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Mon May 1 14:56:43 2023 +1000

    avoid some alloc with Array.Empty (#1898)

[33mcommit 883e20895c83a3a3a1eeac9356d9056c06371bf5[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Mar 31 07:31:35 2023 +1000

    Reinstate `LoggerSinkConfiguration.Sink(ILogEventSink, LogEventLevel)` (#1889)

[33mcommit 8bb02df5433695becc8b93f8e9b1e860b2e6a923[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Mar 31 02:32:03 2023 +1000

    Suppress some warnings, tidy up some namespacing (#1888)

[33mcommit 29d842d7154a56ca22a41e89e5af721d0f18be78[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Mar 30 08:11:02 2023 +1000

    Throw, rather than exit, when any command in the build script fails; fixes #1850 (#1887)

[33mcommit 1264c9b54be9a7fe1ab21d6005b4d9110ea552ef[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Wed Mar 29 15:16:38 2023 +1100

    Microsoft.NET.Test.Sdk 17.5.0 (#1862)
    
    * Microsoft.NET.Test.Sdk 17.5.0
    
    * Microsoft.NET.Test.Sdk drops support for netcoreapp2.1
    
    * Drop redundant constant definitions for `netcoreapp2.1`
    
    ---------
    
    Co-authored-by: Nicholas Blumhardt <nblumhardt@nblumhardt.com>

[33mcommit 72e0b79be214bde649a956df969641dac598b45e[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Wed Mar 29 15:16:26 2023 +1100

    remove MessageTemplateToken.StartIndex (#1882)
    
    * remove StartIndex
    
    * Fix bad merge
    
    ---------
    
    Co-authored-by: Nicholas Blumhardt <nblumhardt@nblumhardt.com>

[33mcommit 3542c296274e4b6fa5a80ceb737bc3b6a4248b0a[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Mar 29 13:53:18 2023 +1000

    README update - fix main branch name

[33mcommit 5af0e30c2436fc96d179e66e3c0b3ebb189a5a7f[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Wed Mar 29 13:45:23 2023 +1100

    remove redundant CallerArgumentExpressionAttribute (#1886)

[33mcommit 6b4fe061f0ad7706317fdc389309d03c15fd3ae3[m
Author: Ivan Maximov <sungam3r@yandex.ru>
Date:   Wed Mar 29 05:45:01 2023 +0300

    Make Alignment and LevelOverride readonly (#1884)

[33mcommit f72a7a328d38f559632ae64aba08dfcf0b1dae44[m
Author: Ivan Maximov <sungam3r@yandex.ru>
Date:   Tue Mar 28 03:50:12 2023 +0300

    Add space settings (#1885)
    
    * Add space settings
    
    * fix

[33mcommit 818741e72d72d567bb1b7befe77b5e02afa1c0a2[m
Author: Ivan Maximov <sungam3r@yandex.ru>
Date:   Mon Mar 27 00:43:12 2023 +0300

    Remove obsolete classes (#1874)

[33mcommit 19d8b1a0b947d70adc7539dbb88772e1410bbab6[m
Author: Ivan Maximov <sungam3r@yandex.ru>
Date:   Mon Mar 13 09:43:48 2023 +0300

    Avoid creating SafeAggregateSink wrapper around empty list (#1878)

[33mcommit ca7470e11122155e9d02a9d024ab2462371fa696[m
Author: Ivan Maximov <sungam3r@yandex.ru>
Date:   Sun Mar 12 13:29:28 2023 +0300

    Use langword in xml comments (#1871)

[33mcommit 10de10567a6abd6417f3a83e6f784fcbf45829de[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Sun Mar 12 21:28:42 2023 +1100

    PublicApiGenerator v11 (#1877)

[33mcommit db990d5eb294f1829c8e27c9ce787e920d84cf89[m
Author: Igor Stojković <stojkovic.igor@gmail.com>
Date:   Thu Mar 9 06:51:30 2023 +0100

    Added ReusableStringWriter (#1771)
    
    * Added ReusableStringWriter
    
    * Simplified ReusableStringWriter
    
    It will now only store the last used formatProvider.
    
    * Made ReusableStringWriter internal
    
    Co-authored-by: Ivan Maximov <sungam3r@yandex.ru>
    
    * Added allocation test for message formatter
    
    * Simplified ReusableStringWriter construction
    
    ---------
    
    Co-authored-by: Igor Stojkovic <igors@nordeus.com>
    Co-authored-by: Ivan Maximov <sungam3r@yandex.ru>

[33mcommit a74da60e1b4ea46bb74667e273624ba749ae45cf[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Sat Mar 4 09:37:13 2023 +1100

    simplify build scripts (#1865)

[33mcommit 8ca0f58d8301b33b62cb98a9721ff599ea061852[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Fri Mar 3 16:49:26 2023 +1100

    Add missing PolySharp changes (#1869)

[33mcommit 33d88b7b998aed58bf2b6eb71b061199064ca163[m
Author: Ivan Maximov <sungam3r@yandex.ru>
Date:   Thu Mar 2 23:59:32 2023 +0300

    Introduce PolySharp (#1845)
    
    * Introduce PolySharp
    
    * remove nullable
    
    ---------
    
    Co-authored-by: Simon Cropp <simon.cropp@gmail.com>

[33mcommit c4f238fa29cf3df110e0f47f19688962ed42c210[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Wed Mar 1 07:11:45 2023 +1100

    Remove redundant reference assemblies (#1866)

[33mcommit c4cd8f66bc92160491340a3178e3fe50c427bf2b[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Wed Mar 1 06:54:41 2023 +1100

    use char delimiters (#1868)

[33mcommit 4985a0b1c9d891df153e10ffea1ccde1a9cb3a38[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Tue Feb 28 22:39:16 2023 +1100

    remove redundant GetPackagingOutputs (#1867)

[33mcommit a4f5f21fa05be58470d014542bd2ab85be783225[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Tue Feb 28 10:13:53 2023 +1100

    move from net461 to net462 (#1863)

[33mcommit 47e16e3be86c4ddb25cdace417abac34006a0d7a[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Sat Feb 25 23:26:53 2023 +1100

    simplify PublicApi_Should_Not_Change_Unintentionally (#1864)

[33mcommit 45d5353da99c6072465a304e3931974ac8426b78[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Sat Feb 25 22:39:29 2023 +1100

    cache empty text token (#1859)

[33mcommit 81c00133cc53f1154bf082544cc42c205202c010[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Sat Feb 25 22:34:20 2023 +1100

    BenchmarkDotNet 0.13.5 (#1861)

[33mcommit f278ff26d731da5d1f0464ca522cc14a0489f153[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Sat Feb 25 17:04:27 2023 +1100

    run tests on mac (#1860)

[33mcommit e23e97905c248a3e79f783bde237723d03d333fb[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Sat Feb 25 14:56:02 2023 +1100

    macOS CI (#1858)
    
    * macOS CI
    
    * Update appveyor.yml

[33mcommit 50bc60fb8b638551746773dc72577124001d0abd[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Sat Feb 4 10:46:29 2023 +1100

    remove FEATURE_GETCURRENTMETHOD const (#1851)
    
    all our TFMs support GETCURRENTMETHOD

[33mcommit f74ba8ef2adc50282c4f910c9d172275bfc6af30[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Sat Feb 4 10:13:18 2023 +1100

    Net471 supports ITuple (#1849)

[33mcommit e0c821de10cc5b3cbd494d417cb1a5aed331d5fb[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Sat Feb 4 09:58:18 2023 +1100

    missing api approval (#1848)

[33mcommit 2d1b190937e5ab65ede733ae3d3fd2ff10046719[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Sat Feb 4 09:48:11 2023 +1100

    fix 461 TargetFramework constants in tests (#1847)

[33mcommit 3e397dcba3b90a65fd7b1a2a0744e095a1c06ccc[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Sat Feb 4 09:15:00 2023 +1100

    fix net471 DisableImplicitFrameworkReferences (#1846)

[33mcommit 7604c1ddd853d729ab9649cd7b97c00747c04e20[m
Author: Andy Gocke <andy@commentout.net>
Date:   Thu Feb 2 21:43:11 2023 -0800

    Annotate Serilog for trimming (#1690)

[33mcommit 0cd7bb643849114a77c7e7414cfcf462798a2341[m
Author: Ivan Maximov <sungam3r@yandex.ru>
Date:   Mon Jan 30 22:59:50 2023 +0300

    Use correct overload with char in JsonFormatter (#1842)

[33mcommit 5e45d78f96d9218ad9ec2194547719b09b9e310b[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Mon Jan 30 16:46:03 2023 +1100

    Leverage null char in JsonFormatter (#1839)

[33mcommit 5b23d0584141a1d79afd9a6e27b2eba5934673bf[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Mon Jan 30 16:43:43 2023 +1100

    avoid casting enum to int where possible (#1841)

[33mcommit 3dc9d1816b77626668b7371fb5e1b6e6ab9d8b59[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Mon Jan 30 16:42:45 2023 +1100

    no point putting _minimumLevel and _levelSwitch on stack (#1840)
    
    ... when using GetEffectiveLevel

[33mcommit 6508cbea8035ad811e6584a6bdbd6fbb7762ea24[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Mon Jan 30 16:41:58 2023 +1100

    remove obsolete PushProperties (#1836)

[33mcommit eb1476e05694b7afa34a02ac918caabdf1909cf3[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Mon Jan 30 16:41:33 2023 +1100

    remove Obsolete JsonFormatter omitEnclosingObject overload (#1834)

[33mcommit b4eaaef5999c74dc862468f0f63f90e08fed4a29[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Mon Jan 30 16:18:13 2023 +1100

    remove obsolete SelfLog.Out (#1837)

[33mcommit 90c2867c34c0d6ef8cf9af01e4b6e3b0005726dc[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Mon Jan 30 15:16:53 2023 +1100

    var in JsonFormatter (#1838)

[33mcommit 32b9f27c751b7e7bfb1171c47f2db389fa86f523[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Mon Jan 30 14:57:40 2023 +1100

    cleanup JsonValueFormatter (#1835)

[33mcommit 09426a75e521bc0d72ce7f45d4c1e0d15baf7534[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Mon Jan 30 14:04:22 2023 +1100

    Move net46 to net461 (#1827)

[33mcommit 630def35c36e07c3821f9b20bc2f2d4c7eec2bfb[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Sat Jan 28 13:29:03 2023 +1100

    use array instead of list when we know the size (#1831)
    
    Co-authored-by: Nicholas Blumhardt <nblumhardt@nblumhardt.com>

[33mcommit 1162daaf3b41534de5e84ce208888990e86911d7[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Fri Jan 27 13:17:47 2023 +1100

    leverage dictionary TryAdd and items constructor

[33mcommit 10e47e087f425e248b6ebaa7f72a8bfe840baf1a[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Fri Jan 27 22:41:20 2023 +1100

    avoid repeated GetType in PropertyValueConverter

[33mcommit 29d48d59be4cc374f2a2d98b8b58903718a4d0ff[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Thu Jan 26 20:41:00 2023 +1100

    Update PropertyValueConverter.cs

[33mcommit dfd4f78d6c0b15ebab202ea1399f41044a021911[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Thu Jan 26 20:39:22 2023 +1100

    Update PropertyValueConverter.cs

[33mcommit b8df974ff6e8664ed6dfe7439f0ba9ad8d6602d5[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Thu Jan 26 20:38:13 2023 +1100

    Update PropertyValueConverter.cs

[33mcommit 0c0209a07eaf4bee5140bec42d7f93f1cce4eb39[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Thu Jan 26 20:37:14 2023 +1100

    Update PropertyValueConverter.cs

[33mcommit 4f374e7e8d88393feb5c6712e5ac0ad30b30e078[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Thu Jan 26 20:36:46 2023 +1100

    Update PropertyValueConverter.cs

[33mcommit c647a42c79cc20dffcaa307ca898f0e098511b33[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Thu Jan 26 20:29:41 2023 +1100

    remove GetTypeInfo from PropertyValueConverter

[33mcommit b435058e5cf6503413a61bb7d44ec8fdcab37b36[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Fri Jan 27 11:36:10 2023 +1100

    comments on Hashtable in MessageTemplateCache
    
    Co-Authored-By: Sergey Komisarchik <15090984+skomis-mm@users.noreply.github.com>

[33mcommit 382c4ea9bcc3efb8e9563c70dd5dcdb4e700dfac[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Fri Jan 27 04:23:26 2023 +1100

    use is Enum for enum check in EnumScalarConversionPolicy (#1825)

[33mcommit 424c189b511765da4bd59620517740a2209381ae[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Thu Jan 26 20:48:34 2023 +1100

    remove redundant nullable suppression

[33mcommit 043c347825e853d0e6471d860c81017b566a5d05[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Thu Jan 26 09:26:36 2023 +1100

    remove FEATURE_HASHTABLE (#1823)

[33mcommit 22af686dea203e95dcda11cebcf2c755e79403a2[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Thu Jan 26 09:11:44 2023 +1100

    remove FEATURE_ASYNCLOCAL (#1822)

[33mcommit 57db345572a8cc5571256f3cea54a0ea474000f7[m
Author: Ivan Maximov <sungam3r@yandex.ru>
Date:   Thu Jan 26 00:54:54 2023 +0300

    Optimize ByteArrayScalarConversionPolicy (#1776)

[33mcommit 580c55fd01bd0dc2c4ace63f1308e4c74fd3d95c[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Wed Jan 25 21:20:20 2023 +1100

    improve GetPropertiesRecursive

[33mcommit 8dc64c5ba6e5f33a1084759c69f2d1c928b5724d[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Wed Jan 25 21:43:52 2023 +1100

    simplify reflection in SurrogateConfigurationMethods

[33mcommit 4b3f60ae191282f6b9a1619dac907fc3af952de9[m
Author: Ivan Maximov <sungam3r@yandex.ru>
Date:   Thu Jan 26 00:47:16 2023 +0300

    Do not allocate strings for TextWriter.Write (#1775)

[33mcommit 10e39fd7c7bf1a6099fc46007ee7149097ad6354[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Thu Jan 26 08:40:08 2023 +1100

    thoughts on switch expressions? (#1818)

[33mcommit 3714dd00fae1eda04695f96ed76bebb80ae79198[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Thu Jan 26 08:22:37 2023 +1100

    remove obsolete PropertyToken constructor (#1819)

[33mcommit 91dfa385ea2f8e63f62d91bb8e88541377b3f8a5[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Thu Jan 26 00:24:38 2023 +1100

    remove redundant GetTypeInfo in EnumScalarConversionPolicy (#1816)

[33mcommit e900c9744673584592e81fb090e497d7f8746288[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Thu Jan 26 00:21:42 2023 +1100

    remove typeinfo from FindConfigurationMethods (#1815)

[33mcommit 2f3bba2edb12f62f6123f368748aa487f81bca9f[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Wed Jan 25 23:30:07 2023 +1100

    remove redundant GetTypeInfo in LoadConfigurationAssemblies (#1817)

[33mcommit d62ec829c22a456ab0f7ae7ad2ba5f915920e4dc[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Wed Jan 25 23:29:04 2023 +1100

    remove net45 support (#1811)

[33mcommit 9876826ce38668ec4ea519bffa86dd9dde7acd7f[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Wed Jan 25 22:27:33 2023 +1100

    use string type keyword consistently

[33mcommit 3f5a9c6fae30629002f125ed9e59f9e423dce5af[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Wed Jan 25 21:12:11 2023 +1100

    add override GetPropertiesRecursive test

[33mcommit 0997cbdd37cfd965c110aba3f05e2793a68f31c6[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Wed Jan 25 21:09:27 2023 +1100

    add ShouldOnlyGetInheritedProperty test

[33mcommit 25c8cdc36d8f798113d952ef8ad97695f038a353[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Wed Jan 25 20:39:25 2023 +1100

    remove duplicate Wheres in FindConfigurationMethods (#1812)

[33mcommit cbb05763da48cbb9c8b422a8c66a06c81519cb3e[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Wed Jan 25 15:36:49 2023 +1100

    remove redundant nullable suppressions (#1810)

[33mcommit f6c98988642a129431a114c32b2e0ea1c7e60e3e[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Wed Jan 25 14:33:53 2023 +1100

    re verify API (#1809)

[33mcommit e8dc8f8e1a2fccb1de7d95d1ad197beb521df44c[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Wed Jan 25 13:31:47 2023 +1100

    remove OutputProperties.GetOutputProperties (#1805)

[33mcommit 28f6ba85108318c1963ee45e62ca2ca9106d48a9[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Wed Jan 25 12:25:29 2023 +1100

    SDK 7 (#1806)

[33mcommit a98793ee3cbd4c76c428c2dbdfddde78289f902d[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Jan 25 11:13:21 2023 +1000

    Remove the obsolete RawFormatter type (#1808)

[33mcommit d27e48ca158824e2e0f03e27ee9a4e087eaea24a[m
Author: Ivan Maximov <sungam3r@yandex.ru>
Date:   Tue Dec 13 12:39:38 2022 +0300

    FEATURE_WRITE_STRINGBUILDER

[33mcommit d74b00781443dc55a4cc63a7f9844d4c3b92cecd[m
Author: Simon <simon.cropp@gmail.com>
Date:   Tue Jan 24 12:53:52 2023 +1100

    remove redundant overrides from LoggerSinkConfiguration

[33mcommit 1617e720dd3fa606f51dd971950943e871979f8d[m
Author: Simon <simon.cropp@gmail.com>
Date:   Tue Jan 24 13:06:56 2023 +1100

    remove Extension of JsonFormatter by subclassing

[33mcommit 5183b416311374eaa5aa60cb6b6a9d6e2902efb0[m
Author: Simon <simon.cropp@gmail.com>
Date:   Tue Jan 24 16:32:53 2023 +1100

    use WriteLine char
    
    better perf options for the underlying writer when it knows it is a single char

[33mcommit 2da1b1f0129775c0b9abdaaa399c8faeb4be9564[m
Author: Simon <simon.cropp@gmail.com>
Date:   Tue Jan 24 16:36:32 2023 +1100

    remove JsonFormatter.Escape

[33mcommit 6e409fe42f8bb4a2164e443d329699c293b4e7b6[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Tue Jan 24 20:28:18 2023 +1100

    drop netstandard1.3 and netstandard1.0 support

[33mcommit 0422af69609f3d191902fe4b9eba579022c1a85f[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Jan 24 09:26:25 2023 +1000

    Major version bump; opens dev for 3.0 development

[33mcommit 7f1e791c411457eb392a9b4833a5545a90a67ba7[m
Author: Ivan Maximov <sungam3r@yandex.ru>
Date:   Sun Dec 4 01:17:01 2022 +0300

    Tokens -> TokenArray

[33mcommit d5cecf3e6b1eca7cef2f42095fff5990a1a34599[m
Author: Ivan Maximov <sungam3r@yandex.ru>
Date:   Thu Dec 15 02:13:08 2022 +0300

    Introduce ScalarValue.Null (#1774)
    
    Introduce ScalarValue.Null

[33mcommit 1fcdf100b9c18ea5f46e31d0d751965e849c26e3[m
Author: dependabot[bot] <49699333+dependabot[bot]@users.noreply.github.com>
Date:   Wed Dec 14 10:21:14 2022 +1100

    Bump Newtonsoft.Json from 13.0.1 to 13.0.2 in /test/Serilog.Tests (#1787)
    
    Bumps [Newtonsoft.Json](https://github.com/JamesNK/Newtonsoft.Json) from 13.0.1 to 13.0.2.
    - [Release notes](https://github.com/JamesNK/Newtonsoft.Json/releases)
    - [Commits](https://github.com/JamesNK/Newtonsoft.Json/compare/13.0.1...13.0.2)
    
    ---
    updated-dependencies:
    - dependency-name: Newtonsoft.Json
      dependency-type: direct:production
    ...
    
    Signed-off-by: dependabot[bot] <support@github.com>
    
    Signed-off-by: dependabot[bot] <support@github.com>
    Co-authored-by: dependabot[bot] <49699333+dependabot[bot]@users.noreply.github.com>

[33mcommit 3093d1caeea63bd92017f5030f6c6533c861caed[m
Author: Ivan Maximov <sungam3r@yandex.ru>
Date:   Sun Dec 4 22:16:27 2022 +0300

    eol

[33mcommit 5d2c065a76103c079c18b31f8186787704578e6b[m
Author: Ivan Maximov <sungam3r@yandex.ru>
Date:   Sun Dec 4 21:44:16 2022 +0300

    Update test/Serilog.ApprovalTests/ApiApprovalTests.cs
    
    Co-authored-by: C. Augusto Proiete <augusto@proiete.com>

[33mcommit d48398463bcb60e5260c7bd9494133146189deb3[m
Author: Ivan Maximov <sungam3r@yandex.ru>
Date:   Sun Dec 4 19:21:53 2022 +0300

    Add API approval test

[33mcommit 94c313a0baa579ac3249083679e7171543a7ed5f[m
Author: Ivan Maximov <sungam3r@yandex.ru>
Date:   Mon Dec 5 01:22:43 2022 +0300

    comments

[33mcommit f08ba747c90b4caa18ecf5bd43b8281311314e97[m
Author: Ivan Maximov <sungam3r@yandex.ru>
Date:   Mon Dec 5 01:20:49 2022 +0300

    Add SequenceValue.Empty

[33mcommit 1f06431b9a5748bd98aebb4b7dbd6bf1199c7c79[m
Author: Ivan Maximov <sungam3r@yandex.ru>
Date:   Mon Dec 5 00:40:24 2022 +0300

    Avoid iterator allocations when working with SequenceValue

[33mcommit 7bbd038c918b32f24bf26fb51d332ce5903f3164[m
Author: Igor Stojkovic <igors@nordeus.com>
Date:   Wed Nov 30 09:15:41 2022 +0100

    Avoided IEnumerator allocation (#1769)

[33mcommit c7eaa82354f5155992d748bb769bd61636b159c1[m
Author: Ivan Maximov <sungam3r@yandex.ru>
Date:   Tue Oct 25 00:40:13 2022 +0300

    Change exception message (#1762)

[33mcommit 38aecabf9dc28aeb402814eb6df470eba2630b91[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Sep 13 14:28:55 2022 +1000

    Dev version bump [skip ci]

[33mcommit 47df92d36a2393276eb9a0d696db93fcb850b450[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Sep 13 11:32:35 2022 +1000

    Fix nullability of LogContext.PushProperty value arg (#1751)

[33mcommit 7740c90e71c2121ce0f12e4fa57d44f6aa7ce9da[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Sep 12 17:35:49 2022 +1000

    Initial support for `IAsyncDisposable` sinks (#1750)
    
    * Support IAsyncDisposable.DisposeAsync() on Logger, and add Log.CloseAndFlushAsync()
    
    * Add tests for Log.CloseAndFlushAsync()
    
    * Seal DisposeAggregatingSink
    
    * Fix tests
    
    * Add missing ConfigureAwait(false)
    
    * Implement IAsyncDisposable on remaining sink wrapper types
    
    * More test coverage

[33mcommit 4d13be50c03e14b6072043799dc7e5dbe4139a19[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Fri Sep 9 13:03:44 2022 +1000

    guard against null (#1742)
    
    * guard against null
    
    * Create ArgumentNullTests.cs
    
    * Update Guard.cs

[33mcommit fd36c3e40f916ea6e141d2623d3e27b8d7eca921[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Wed Sep 7 09:07:03 2022 +1000

    remove redundant type names (#1741)
    
    * remove redundant type names
    
    * Update KeyValuePairSettings.cs

[33mcommit 3bd65ef2e462650b14352c05646cb3dfac2f98ed[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Mon Sep 5 16:29:59 2022 +1000

    remove redundant LogEventLevel (#1745)

[33mcommit 1819ee76bc49437055266b4e29d150eb6aba60e2[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Mon Sep 5 16:26:51 2022 +1000

    refs (#1747)

[33mcommit e0c533d1eea203b4c5af0b3a0e0388a1cdd96b37[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Mon Sep 5 16:26:16 2022 +1000

    minor cleanups (#1746)

[33mcommit 70e729de9a917b15c7917c66515cc62ebe16d70b[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Thu Sep 1 10:07:32 2022 +1000

    use some patterns (#1739)

[33mcommit c5a5b44daf11d5be8add28b862103d949e1a16aa[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Thu Sep 1 07:19:15 2022 +1000

    global usings (#1736)

[33mcommit bed99cf1b1e484ca182f08857ad8d5723129c13c[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Wed Aug 31 09:08:05 2022 +1000

    leverage ITuple interface (#1733)
    
    * leverage ITuple interface
    
    * IVALUETUPLE
    
    * resolve merge conflict
    
    * missed a conflict
    
    * const should be ITUPLE

[33mcommit 5a69b469ca861efbb506fa74c7eda8276fd74c93[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Sat Aug 27 07:17:58 2022 +1000

    forgot to remove some Nullable>enable (#1735)
    
    * forgot to remove some Nullable>enable
    
    * missed some #nullable enable

[33mcommit 365adaedffa22e1138d3a180288b2bbe9ec243b5[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Fri Aug 26 07:36:54 2022 +1000

    file scoped namespaces (#1734)

[33mcommit 9aff35ab152a11ec7d42aa89114bf1776621d2ba[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Thu Aug 25 11:15:02 2022 +1000

    use some compound assignments (#1732)

[33mcommit 0ecc14b266bf1e17c283190b43702b57c985a4e0[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Thu Aug 25 11:14:20 2022 +1000

    use some lambda expressions (#1731)

[33mcommit ec15a62e99c0ce471052b1c45af049ce03d7432e[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Thu Aug 25 10:40:32 2022 +1000

    suppress CheckEolTargetFramework (#1730)
    
    so we dont get [NETSDK1138] The target framework 'netcoreapp2.1' is out of support and will not receive security updates in the future. Please refer to https://aka.ms/dotnet-core-support for more information about the support policy.
    
    Co-authored-by: Nicholas Blumhardt <nblumhardt@nblumhardt.com>

[33mcommit 3214c474b60457c64d2c471ca5a7f57cb11c2ce9[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Thu Aug 25 10:07:49 2022 +1000

    Finish nullable. now enabled globally (#1729)
    
    * nullable in LogEventProperty
    
    * nullable in JsonValueFormatter
    
    * nullable in EventProperty
    
    * nullable in LogContext
    
    * enable nullable for EnricherStack
    
    * remove "#nullable enable" and enable globally
    
    * Update Logger.cs

[33mcommit fb964f8b8449d80c563f1e3299d4e372d0f7dc1f[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Wed Aug 24 08:40:43 2022 +1000

    rename ImmutableStack<ILogEventEnricher> to EnricherStack (#1728)

[33mcommit 635667b054f2d699ebcecb8acfe7816da099ca80[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Thu Aug 18 07:54:30 2022 +1000

    Nullable annotations for destructuring policies (#1720)

[33mcommit 1e16547f507514e3381c07b590e083593631f1b1[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Wed Aug 17 16:44:03 2022 +1000

    SettingValueConversions.ConvertToType() value cant be null (#1721)
    
    * SettingValueConversions.ConvertToType() value cant be null
    
    * Update KeyValuePairSettings.cs
    
    * Update SettingValueConversionsTests.cs

[33mcommit 7c59f8408baba146702a5ac7cdbd0044af5de290[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Tue Aug 9 18:12:13 2022 +1000

    nullable in MessageTemplateParser (#1722)

[33mcommit 15feab4fc4d3cdbd747802189ef47ad1bb4352e0[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Tue Aug 9 18:05:55 2022 +1000

    use some auto properties (#1723)

[33mcommit b54298532ba778ac34b97615667b598b7dda029b[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Tue Aug 9 18:03:39 2022 +1000

    update xunit (#1724)

[33mcommit 382660aef1d2e97c56aae56ccf74d9c6b886ba4d[m
Author: Ivan Maximov <sungam3r@yandex.ru>
Date:   Tue Aug 9 11:02:31 2022 +0300

    Introduce file-scoped namespaces (#1725)

[33mcommit 70237f465b164ce9e9c0967f2029e4dd69f1b389[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Tue Aug 2 12:58:39 2022 +1000

    minor csproj cleanup (#1712)
    
    * minor csproj cleanup
    
    * Remove redundant originator key file property
    
    Co-authored-by: Nicholas Blumhardt <nblumhardt@nblumhardt.com>

[33mcommit de4544d53245934e4d35fcae70204cb0122a3394[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Tue Aug 2 12:55:34 2022 +1000

    format  and formatProvider can be null (#1715)
    
    * format  and formatProvider can be null
    
    * Added missing space
    
    Co-authored-by: Nicholas Blumhardt <nblumhardt@nblumhardt.com>

[33mcommit 7ec90ba316321d2fae29679995b3d9a284b19b2e[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Tue Aug 2 12:35:08 2022 +1000

    nullables in SettingValueConversions CallableConfigurationMethodFinder KeyValuePairSettings (#1717)

[33mcommit f447af38361a7fdfbf028da54ec781c9907903b3[m
Author: Ivan Maximov <sungam3r@yandex.ru>
Date:   Tue Aug 2 02:21:27 2022 +0300

    remove ! operator from nulls in conditions (#1719)

[33mcommit 1c926f031b4e1d72cb1db456ecc809258a9859f0[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Fri Jul 29 13:32:30 2022 +1000

    out ScalarValue can be null (#1716)

[33mcommit 1123784fe1a14e837ce63f46c47579e4db51c3d9[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Fri Jul 29 09:16:01 2022 +1000

    ScalarValue. value can be null (#1713)

[33mcommit 9f0c43c6e0f876216a940e0261257d4fefff5018[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Thu Jul 28 09:06:20 2022 +1000

    add missing VALUETUPLE check (#1711)

[33mcommit 5307e3572a9d6b393ac3ada7719b5c4c6af6a811[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Jul 28 09:04:41 2022 +1000

    Bring Log.* methods exception nullability into line with ILogger (#1707)

[33mcommit 255e76e2532bd807c0798e291d11b224bdfa151b[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Wed Jul 27 19:44:49 2022 +1000

    update Microsoft.NET.Test.Sdk and nullable (#1709)

[33mcommit f3349c1169c599ed9be24d9ed8e79b86f21f63ce[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Wed Jul 27 19:40:31 2022 +1000

    remove BuildNativeUWP.ps1 from sln files (#1708)

[33mcommit 921037a020e208986ee12e4551295228ec97ff8f[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Tue Jul 26 15:10:52 2022 +1000

    DateOnly and TimeOnly support (#1702)
    
    * .
    
    * .
    
    * Update global.json
    
    * FEATURE_DATE_AND_TIME_ONLY
    
    * Update PropertyValueConverter.cs
    
    Co-authored-by: Nicholas Blumhardt <nblumhardt@nblumhardt.com>

[33mcommit f986e6245bc1c8ea137a5b5d1174c340ccecd1a4[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Jul 26 08:17:13 2022 +1000

    Update NuGet publishing key

[33mcommit e01af659c77681d6b2614e335be33be1feb54d6c[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Jul 26 08:09:47 2022 +1000

    Minor version bump - new target/features added

[33mcommit 6eae5ac401a0b3ef7f8162909e66472aa13434af[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Jul 26 08:09:10 2022 +1000

    Fix #1686 - nullable annotations for PropertyEnricher (#1704)

[33mcommit ccd6e9c94415d518fe3ce5033842652182841c6d[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Jul 26 07:42:18 2022 +1000

    More nullable annotation updates (#1700)

[33mcommit c4ec76729a67bc24d4553ad2871ee6c220fed8b7[m
Author: Ivan Maximov <sungam3r@yandex.ru>
Date:   Tue Jul 26 00:33:38 2022 +0300

    Remove wrong XDOC tag (#1699)
    
    * Remove wrong tag
    
    * fix

[33mcommit 45386bba0b425286d9747abf054f7e0388f976a8[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Tue Jul 26 07:24:00 2022 +1000

    add explicit net6 target (#1703)
    
    * add explicit net6 target
    
    * Update global.json
    
    * Update BuildNativeUWP.ps1
    
    * Update BuildNativeUWP.ps1
    
    * remove uwp

[33mcommit 81e9dee4aaa86538f9e754fa9887a8603800274a[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Jun 30 09:04:59 2022 +1000

    Remove redundant `.` from example message
    
    Fixes #1694

[33mcommit fabc2cbe637c9ddfa2d1ddc9f502df120f444acd[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Wed May 4 16:25:28 2022 +1000

    update refs (#1674)

[33mcommit 7521f172ec66d91a1034d6ea2d343793ec193a3e[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Apr 24 10:15:33 2022 +1000

    Dev version bump [skip ci]

[33mcommit b138671618b743438f8c4e547af9ed36a5ce65a6[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Apr 24 09:34:24 2022 +1000

    Fix trailing '>' in ILogger.cs; enclose example code in <code> tags

[33mcommit ca8e100137eb95e07cfa21eda7b5c5f450defb27[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Apr 23 13:33:52 2022 +1000

    Fix nullable annotation on Log.ForContext(); minor nullability tweaks

[33mcommit 5c425c5bbb57789e51df8c3ab420aa21ca78857e[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Apr 23 08:24:55 2022 +1000

    Revert "Update ILogger XML documentation"
    
    This reverts commit 5de700278ac59403dff3d8acab91edb2eff22ef6.

[33mcommit 5de700278ac59403dff3d8acab91edb2eff22ef6[m
Author: Eric Hiller <mrraptor98@gmail.com>
Date:   Sun Apr 3 06:12:20 2022 -0500

    Update ILogger XML documentation
    
    - Update ILogger examples XML documentation to use `<code>`
    - Fix an errant `>`

[33mcommit e1cdad57d96dc1d5eb2c1077a2d918965e82488d[m
Author: Kareem Zedan <kareem335@yahoo.co.uk>
Date:   Sun Jan 23 21:17:52 2022 +0000

    Fixed issue

[33mcommit 1aabe1d6bde10382233fb2a50e0e2c6e0c9b8287[m
Author: JinsPeter <jins.peter@redblacksoftware.com>
Date:   Mon Dec 6 19:24:09 2021 +0530

    Corrected Example in documentation
    
    The example in the documentation was using a wrong name for the attribute. Corrected the usage in the example

[33mcommit 17043b56ce97becdbfd2a503ffc16fc77f52285c[m
Author: Sergey Komisarchik <ylemsoul@gmail.com>
Date:   Mon Nov 15 18:25:20 2021 +0300

    (ReadOnly)Memory<byte> scalar policy; handle NSE at PropertyValueConverter.GetProperties()

[33mcommit 13dd8cfc5ee47a841128beaef853b3798464f807[m
Author: C. Augusto Proiete <augusto@proiete.com>
Date:   Wed Sep 8 07:18:33 2021 -0300

    Update issue template to point to Usage help wiki page

[33mcommit aa5645f840e2da0f55e9c30cf04e1bab6f20ff99[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Aug 20 10:05:40 2021 +1000

    Various new features now on `dev` (nullables, and key-value-pair settings with array values)

[33mcommit 697a23189e3068bfe0da1460a2676913239d2757[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Fri Aug 20 09:31:24 2021 +1000

    Moar nullables (#1597)
    
    
    * Update PropertyBinder.cs
    
    * Update LoggerConfiguration.cs
    
    * Update AggregateSink.cs
    
    * Update ConditionalSink.cs
    
    * Update DisposeDelegatingSink.cs
    
    * Update DisposingAggregateSink.cs
    
    * Update FilteringSink.cs
    
    * Update RestrictedSink.cs
    
    * Update SafeAggregateSink.cs
    
    * Update SecondaryLoggerSink.cs
    
    * Update LoggingLevelSwitch.cs
    
    * Update PropertiesOutputFormat.cs
    
    * Update JsonFormatter.cs
    
    * Update JsonValueFormatterTests.cs
    
    * Update Log.cs

[33mcommit e10d3381084b2c12815024e58faef1775cd050e2[m
Author: Tomasz Stochmal <stochmal@gmail.com>
Date:   Thu Aug 19 06:57:59 2021 +0200

    Add support for collections/`string[]` to key-value settings (#1561)
    
    * fixed exception
    
    * added unit test and using comma as seperator
    
    * added empty array return
    
    * removed internal
    
    * added support for int[] support
    
    * support any collection type
    
    * Remove unnecessary newline
    
    Co-authored-by: Tomasz Stochmal <tomasz.stochmal@iress.com>
    Co-authored-by: Nicholas Blumhardt <nblumhardt@nblumhardt.com>

[33mcommit 5e93d5045585095ebcb71ef340d6accd61f01670[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Jun 23 14:42:54 2021 +1000

    Fix testing of net5.0 target; disallow warnings in test projects and tidy accordingly (#1596)

[33mcommit 40ecb4cad8f35dafaec7ca133dbd91f4fe2a35ba[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Wed Jun 23 14:32:17 2021 +1000

    use some pattern matching (#1593)

[33mcommit 3c49606448f32efb0f46765c7c224030abfdfe0d[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Jun 23 11:14:08 2021 +1000

    Make Nullable a private dependency; squeeze in a net5.0 target (#1595)

[33mcommit 426781085d3eb05afe6b56e22b5df8ebe7ddbaf9[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Tue Jun 22 15:02:36 2021 +1000

    Use Assert.False() where appropriate (#1594)

[33mcommit 3fcd83816c13fe592e677a9f50e86a9d7dbd839e[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Tue Jun 22 15:02:06 2021 +1000

    Make tests use nullable reference types (#1591)
    
    * make tests nullable
    
    * Update MessageTemplateTextFormatterTests.cs
    
    * Update MessageTemplateTextFormatterTests.cs
    
    * remove some } = null!;
    
    * Update LogContextTests.cs
    
    * missing log events
    
    * Update DelegatingSink.cs

[33mcommit 6b4558e89af15452a973f5fa6545996cb1d9ea8f[m
Author: Al Idian <al.idian@me.com>
Date:   Mon Jun 21 21:27:21 2021 -0700

    Write to SelfLog to warn about settings with missing assembly references (#1539) (#1592)

[33mcommit a899a703d2966628860d1d64c56048da2e5ba947[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Mon Jun 21 10:00:58 2021 +1000

    Add nullable for ilogger and implementations (#1588)
    
    * add nullable for ilogger and implementations
    * narrow down nullables
    * make ILogEventEnricher not null

[33mcommit b6bb807422c746413d029968567964e6f0515c4b[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Tue Jun 15 14:34:25 2021 +1000

    Update DummyConsoleSink.cs

[33mcommit 1d5deec933ff5161fcdaa05826edd28f5dc409eb[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Tue Jun 15 11:17:56 2021 +1000

    make TestDummies use nullable

[33mcommit d6b29ce3ca1a909f07340ecdd4ad34bc39789cd8[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Tue Jun 15 11:57:13 2021 +1000

    make MethodOverloadConventionTests nullable

[33mcommit bbcbec79ac17179c5af8dcd06df7e3c2c465162a[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Tue Jun 15 11:34:15 2021 +1000

    use nullable in Serilog.PerformanceTests

[33mcommit 2aabe64a323ff449a2a483968e03d57e4d0bb19d[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Sat May 15 13:25:11 2021 +1000

    move to target typed new

[33mcommit ac685296c5bbf2362a0169f88acb1d8d52a8c1d7[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue May 11 20:47:39 2021 +1000

    Update expired NuGet publishing key

[33mcommit 3aa1a76f2cfd60be53a7341eb078aaf3bb4d43cf[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue May 11 20:24:01 2021 +1000

    Fix some tests

[33mcommit 8d084254dd4a7af6871e29918366719f127e1bb6[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue May 11 19:59:39 2021 +1000

    Build badge fixups

[33mcommit 7906310156d0373653952e3278bb105676b60d03[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue May 11 17:14:08 2021 +1000

    .NET 5.0.202 SDK, main branch change

[33mcommit 9669a3fd0d930b38ad16fb0132c8c7829b50283c[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Tue May 4 11:37:07 2021 +1000

    remove some redundant variables

[33mcommit 34bd2125e8a0e54b350aa6391298cb40920d7ff1[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Tue May 4 11:27:55 2021 +1000

    update some references

[33mcommit e9c90942b4c48ee821dbbac37d5fdb6e9c7134aa[m
Author: Avisss <71761763+Avisss@users.noreply.github.com>
Date:   Mon Feb 8 12:27:49 2021 +0600

    more precise logevent timestamp

[33mcommit e890a3dc5a77c1ae782f7104ee51f5874d55bf45[m
Author: Juliano Goncalves <juliano.goncalves@poatek.com>
Date:   Thu Dec 10 19:03:31 2020 -0300

    Fix miswording on 'AsScalar' destructuring methods
    
    "...event when..." -> "...even when..."

[33mcommit e82213e98ece04bdfcb13d066f91b987ca4921c6[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Thu Nov 5 08:46:20 2020 +1100

    make some local functions static to avoid scope capture (#1503)
    
    * make some local functions static to avoid scope capture
    
    * make some local functions static

[33mcommit 24d623769fd7a00b348af3dac0b875bf23b3aea5[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Sep 10 12:20:38 2020 +1000

    Update CHANGES.md

[33mcommit 38fb475d5eb5d8f1cb00d62d4e1ac563de6aa381[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Sep 10 12:20:09 2020 +1000

    Update Serilog.csproj

[33mcommit 33f1fd59307838f280579f89f07cc809bec348e1[m
Merge: d98fca51 94d9e1ab
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Sep 10 09:21:13 2020 +1000

    Merge pull request #1452 from rafaelsc/bugfix/jsonFormater
    
    Record stringified property as `""` when `ToString()` returns `null` in JsonFormatters

[33mcommit 94d9e1ab332808f6c48592dfed6c222324c868e5[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Sep 10 09:06:14 2020 +1000

    Nitpicks - whitespace, slightly more specific test description

[33mcommit d98fca51063cf6d3345a3171dda28048e0469748[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Aug 17 08:32:26 2020 +1000

    Update and reformat CHANGES.md

[33mcommit 23cae3c19b410b206325a66f99cac0ee0242dce6[m
Author: Rafael Cordeiro <rafaelsc@rafaelsc.net>
Date:   Tue Aug 4 22:03:59 2020 -0700

    Remove testes that have data that not happens in Serilog

[33mcommit 1a6f9b1bb83c89f2546ec61ecb83f5c1b039cedf[m
Merge: 5c230259 1582b861
Author: Rafael Cordeiro <rafaelsc@rafaelsc.net>
Date:   Tue Aug 4 19:16:55 2020 -0700

    Merge branch 'dev' into bugfix/jsonFormater

[33mcommit 1582b8611f8a8cbff38f6efdec93026e17d8ecc2[m
Merge: 88c7d945 308208e1
Author: Rafael Sliveira Cordeiro <rafaelsc@rafaelsc.net>
Date:   Mon Aug 3 23:17:54 2020 -0700

    Merge pull request #15 from serilog/dev
    
    Update Dev

[33mcommit 308208e12f722ad6c48394586965ea957aa37c02[m
Merge: c10e3a81 89ec7f32
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Aug 2 14:49:01 2020 +1000

    Merge pull request #1472 from skomis-mm/subMinLvl
    
    Don't inherit parent Minimum Level for inline sublogger configuration

[33mcommit 89ec7f326b20ede2d14f61379102795335393605[m
Author: Sergey Komisarchik <skomisarchik@usetech.ru>
Date:   Sat Aug 1 17:18:04 2020 +0300

    removed inheriting minimum level from other places

[33mcommit b60795c5755a3dd05b41768c9715580cf05d3a09[m
Author: Sergey Komisarchik <skomisarchik@usetech.ru>
Date:   Fri Jul 31 15:53:36 2020 +0300

    failing test fix try

[33mcommit 7b7585d883329d5edb74c3108cd112ee026226bf[m
Author: Sergey Komisarchik <skomisarchik@usetech.ru>
Date:   Fri Jul 31 14:22:15 2020 +0300

    don't inherit parent minimum level for inline sublogger configuration

[33mcommit 5c23025981d52ad60ad245249652426c969d5bec[m
Merge: 5abcae08 c10e3a81
Author: Rafael Sliveira Cordeiro <rafaelsc@rafaelsc.net>
Date:   Thu Jul 30 10:55:28 2020 -0700

    Merge branch 'dev' into bugfix/jsonFormater

[33mcommit c10e3a81dbd555f2ab89730a9b106e5d91cddff7[m
Merge: 94a934dd ae7dd354
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Jul 30 19:24:05 2020 +1000

    Merge pull request #1467 from nblumhardt/tostring-null
    
    A null result from ToString() stringifies as an empty string instead of null

[33mcommit ae7dd3543a45be7c8f62aead3ab79447f123e188[m[33m ([m[1;31morigin/tostring-null[m[33m)[m
Merge: 20454e0e 94a934dd
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Jul 30 18:51:39 2020 +1000

    Merge branch 'dev' into tostring-null

[33mcommit 94a934ddbe71bf028813b6fd122cfcc67266ca30[m
Merge: bb6188a9 f906d6b1
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Jul 30 18:47:20 2020 +1000

    Merge pull request #1466 from nblumhardt/wrapper-sinks-refactor
    
    Refactor wrapper sinks

[33mcommit 5abcae08177c43d97bf9de64efd48e3bc1d3d4e2[m
Author: Rafael Cordeiro <rafaelsc@rafaelsc.net>
Date:   Wed Jul 29 23:17:01 2020 -0700

    Update Tests

[33mcommit 96e59e412758f2d47980b30e38fe6373c4e82f6d[m
Author: Rafael Cordeiro <rafaelsc@rafaelsc.net>
Date:   Wed Jul 29 23:01:06 2020 -0700

    Changes to have the same behavior of #1467

[33mcommit 20454e0ef262071c6fe81e1ec9c5d1a3407a83b7[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Jul 26 12:57:07 2020 +1000

    Fix test; capture objects that return null from ToString as empty-string scalars

[33mcommit 3893b147d87b8030191d7036a8d13fa8db3c3d44[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Jul 26 12:39:04 2020 +1000

    Modifies the behavior introduced in #1427 - a null result from ToString() now renders as an empty string instead of null

[33mcommit f906d6b1a442e0261007a01efa53a7651744d017[m[33m ([m[1;31morigin/wrapper-sinks-refactor[m[33m)[m
Merge: 2c2ddbec 7fc99ab5
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Jul 26 12:22:05 2020 +1000

    Merge branch 'update-logger-sink-configuration-wrap' of https://github.com/augustoproiete-forks/serilog--serilog into wrapper-sinks-refactor

[33mcommit 2c2ddbec76fc15c2b2cd37f9b20f2a2fedf209d9[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Jul 26 12:13:29 2020 +1000

    Break the inheritance link between SafeAggregateSink and DisposingSafeAggregateSink; remove one level of indirection in DisposeWrappingSink, now renamed DisposeDelegatingSink

[33mcommit d823fa1ba75b9a21eb79de2be47848781f704182[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Jul 26 10:11:34 2020 +1000

    Code style/consistency updates

[33mcommit bb6188a93fc64b31aa87ee4c7d130f2a2acac78e[m
Author: JanEggers <JanEggers@users.noreply.github.com>
Date:   Sun Jul 26 01:49:30 2020 +0200

    Fix #1463 - LoggerSinkConfiguration.Wrap() fails to dispose wrapped sinks in some scenarios  (#1465)

[33mcommit 9b4b1d8c2d6583ac8ad67833014fdc3105c0d5e3[m
Merge: 22271f2e 770f888b
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Jul 25 16:27:10 2020 +1000

    Merge pull request #1442 from skomis-mm/srcCtxMatch
    
    Source context matching optimizations

[33mcommit b413168b87d5aa9f5743dc3a15be46d57d2ab434[m
Author: Rafael Cordeiro <rafaelsc@rafaelsc.net>
Date:   Mon Jun 29 16:44:19 2020 -0700

    Performance Improvements

[33mcommit 6a62806c77bdc65abf2f57223f4a1e804bbb85de[m
Author: Rafael Cordeiro <rafaelsc@rafaelsc.net>
Date:   Fri Jun 26 23:52:26 2020 -0700

    BugFix Exception when using Stringify. Fix #1451

[33mcommit 88c7d945b4406214906737439d2b2bf13d224649[m
Merge: 2d81a61e 22271f2e
Author: Rafael Sliveira Cordeiro <rafaelsc@rafaelsc.net>
Date:   Fri Jun 26 21:40:10 2020 -0700

    Merge pull request #10 from serilog/dev
    
    Merge Last Changes

[33mcommit 7fc99ab51a0f36a19b0074ab828b1f47436f3c3a[m
Author: C. Augusto Proiete <augusto@proiete.com>
Date:   Wed Jun 17 22:51:15 2020 -0300

    Update `LoggerSinkConfiguration.Wrap()` to use `AggregateSink`
    Relates to #1441

[33mcommit 22271f2e2b31e8dcd47128bde8db4287e8425557[m
Merge: 8f2263cb d116b6bf
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Jun 12 10:08:45 2020 +1000

    Merge pull request #1445 from JakenVeina/logger-minimum-level-configuration-override-docs-update
    
    Updated docs regarding LoggerMinimumLevelConfiguration.Override()

[33mcommit d116b6bf8c68ef4ed2f4f5f0fa46690c9a6e6fa8[m
Author: JakenVeina <jakenveina@yahoo.com>
Date:   Tue Jun 9 20:54:35 2020 -0500

    Updated docs for `LoggerMinimumLevelConfigration.Override()` to indicate that this API is not supported for configuration of sub-loggers.

[33mcommit 770f888b919187cc0bc4f7226ce5a2b93270613e[m
Author: Sergey Komisarchik <skomisarchik@usetech.ru>
Date:   Tue Jun 2 01:00:12 2020 +0300

    copyright years update

[33mcommit 5e3be7ddf9ffdd1952c142a56a838b742b1f17b6[m
Author: Sergey Komisarchik <skomisarchik@usetech.ru>
Date:   Tue Jun 2 00:54:12 2020 +0300

    source context match benchmark results

[33mcommit b5c3fa199c4074679675f1ca102224937e23ec2c[m
Author: Sergey Komisarchik <skomisarchik@usetech.ru>
Date:   Mon Jun 1 22:17:10 2020 +0300

    source context match optimization

[33mcommit 8f2263cb9e97146d9e180783dba601d9902eeeaf[m
Merge: 5e216475 194ce557
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue May 26 10:16:37 2020 +1000

    Merge pull request #1435 from skomis-mm/defaultIntf
    
    Default `ILogger` implementation for `netstandard2.1`+

[33mcommit 194ce557c8eec69ba969e50f9f963ae41fc31319[m
Author: Sergey Komisarchik <skomisarchik@usetech.ru>
Date:   Mon May 25 15:40:31 2020 +0300

    ILogger.Write() fix: forward BindMessageTemplate(..) call to interface implementation

[33mcommit ab2b3e4628832bde7c7a11dced9208cb5dc3b6d1[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon May 25 09:29:49 2020 +1000

    Minor version bump - new feature

[33mcommit d49244bc2d92c6ad22106f2a663215c734e2f45b[m
Author: Sergey Komisarchik <skomisarchik@usetech.ru>
Date:   Thu May 21 15:43:56 2020 +0300

    avoid aliases for Build.ps1; set lf eol for all by default

[33mcommit acff505f838483eedd809b2b6752cb933c0e005c[m
Author: Sergey Komisarchik <skomisarchik@usetech.ru>
Date:   Thu May 21 15:42:14 2020 +0300

    remaining ILogger default implementations

[33mcommit 4f61660bd4d25dc57767790f44afddfc19b446ac[m
Author: Sergey Komisarchik <skomisarchik@usetech.ru>
Date:   Wed May 20 19:15:20 2020 +0300

    more cleanup; reorder TFMs with .netcore first in order to make Omnsharp / VS Code happy

[33mcommit 9c9d0972c0f94e627d44bccffe2b136dcd7f5a5e[m
Author: Sergey Komisarchik <skomisarchik@usetech.ru>
Date:   Wed May 20 02:10:04 2020 +0300

    appveyor_perftest.yml fix

[33mcommit dcc6dc05800fffe722c2d3263d3baaa14b03a529[m
Author: Sergey Komisarchik <skomisarchik@usetech.ru>
Date:   Wed May 20 01:32:07 2020 +0300

    fix appveyor.yml

[33mcommit 63b0391055659c59770020e006ed95353162749a[m
Author: Sergey Komisarchik <skomisarchik@usetech.ru>
Date:   Sun May 17 21:07:43 2020 +0300

    netstandard2.1 with default ILogger implemenations
    along with:
    
    - build scripts actualization
    - added latest LTS .net framework / core targets
    - misc cleanup

[33mcommit 2050f4d258308ecd2aabcaefb5bd96100e3c637d[m
Author: Rafael Cordeiro <rafaelsc@rafaelsc.net>
Date:   Fri May 8 17:32:50 2020 -0700

    Add more tests

[33mcommit 2d81a61e743b68235bb4d82b08a18cb36a8a90b1[m
Merge: 1818ec6b 5e216475
Author: Rafael Sliveira Cordeiro <rafaelsc@rafaelsc.net>
Date:   Wed May 6 15:54:23 2020 -0700

    Merge pull request #8 from serilog/dev
    
    Improves XML documentation (#1429)

[33mcommit 5e2164756efb6023a548b4eb91a71ed7edc3d9e1[m
Author: Rafael Sliveira Cordeiro <rafaelsc@rafaelsc.net>
Date:   Mon May 4 19:02:30 2020 -0700

    Improves XML documentation (#1429)
    
    * Add Exception to XML Documentation.
    Code Format.
    Code Refactor
    
    * Fix XML Documentation
    
    * Update XML Documentation
    Code Format
    
    * Fix some legacy invalid XDOC
    
    * Fix some legacy invalid XDOCs
    
    Co-authored-by: Nicholas Blumhardt <nblumhardt@nblumhardt.com>

[33mcommit 1818ec6baad4a7524707c40e66e6d760b80c9697[m
Merge: e2c828a2 ed1fc895
Author: Rafael Sliveira Cordeiro <rafaelsc@rafaelsc.net>
Date:   Fri May 1 18:03:54 2020 -0700

    Merge pull request #7 from serilog/dev
    
    Merge

[33mcommit ed1fc89570450eafd09d225cdf322fc616d4ae37[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sat May 2 07:48:13 2020 +1000

    Update NuGet.org key (Serilog Core publishing only)

[33mcommit aff8fd7951f1b9fc3b9dc3569e40f0dcdaae159e[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sat May 2 07:35:04 2020 +1000

    Remove obsolete issue templates

[33mcommit 9773438c90e8e1b77423ac4db2d27eb2dd33a3d2[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sat May 2 07:34:46 2020 +1000

    Remove obsolete issue templates

[33mcommit 645c173197cdae3e38e825ebd013b7fe97ea27c1[m
Merge: 2c7dbbfa 40857b62
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Apr 30 13:03:20 2020 +1000

    Merge pull request #1427 from rafaelsc/bugfix/stringifyException
    
    Fix: A bad designed Class can cause an Exception when using Stringify

[33mcommit e2c828a289d626cb3810f99baf88384e9ce257d2[m
Merge: dd519425 2c7dbbfa
Author: Rafael Sliveira Cordeiro <rafaelsc@rafaelsc.net>
Date:   Wed Apr 29 19:09:04 2020 -0700

    Merge pull request #6 from serilog/dev
    
    Merge

[33mcommit 40857b626975186ce4f9179a343ef3bcbf2b4004[m
Author: Rafael Cordeiro <rafaelsc@rafaelsc.net>
Date:   Wed Apr 29 18:59:03 2020 -0700

    BugFix Exception when using Stringify. Fix #1426

[33mcommit 59047366176d2549848d675d87932eaabb4c5e87[m
Author: Rafael Cordeiro <rafaelsc@rafaelsc.net>
Date:   Wed Apr 29 18:58:34 2020 -0700

    Add Testes to bad designed Classes

[33mcommit 2c7dbbfad6c607adad6a57f86e8cd680ddf620a1[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Apr 27 11:27:21 2020 +1000

    Minor README tweaks; encourage bug reports and suggestions via the tracker

[33mcommit 0843483daf7bd6f793dbc14dda475110466f8361[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Apr 27 09:01:19 2020 +1000

    Update issue templates
    
    These were due for an update. Note, they now direct people to Stack Overflow for usage help.

[33mcommit 720379078ef90def0519fb66bc1db96833e4a459[m
Merge: 8ae332d9 d1421f9d
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Apr 24 08:23:01 2020 +1000

    Merge pull request #1392 from nblumhardt/publish-symbols
    
    Reenable NuGet symbol publishing

[33mcommit dd5194252eedf7df5f7238f2169bb556bde8ebbd[m
Merge: 90cdec6c 8ae332d9
Author: Rafael Sliveira Cordeiro <rafaelsc@rafaelsc.net>
Date:   Thu Feb 6 21:33:55 2020 -0800

    Merge pull request #5 from serilog/dev
    
    Merge

[33mcommit d1421f9d059d7670ce420879b5f799902adbd3c4[m[33m ([m[1;31morigin/publish-symbols[m[33m)[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Jan 7 07:49:15 2020 +1000

    Reenable NuGet symbol publishing

[33mcommit 8ae332d983b31044f4da0fa34e3b9cb85ba68bc9[m
Merge: c357da4e db18cbfc
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Nov 25 11:55:14 2019 +1000

    Merge pull request #1374 from rafaelsc/improvements/unitTest/parser
    
    Improve Serilog.Parsing Unit Tests and Code Coverage - Take 2

[33mcommit c357da4e7e2e696473feb1a31f4677fbfe974ecc[m
Merge: c0777758 be0b6782
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Oct 29 14:16:21 2019 +1000

    Merge pull request #1378 from nblumhardt/fix-ubuntu-dotnet-install
    
    Follow redirect to download dotnet-install.sh

[33mcommit be0b678246f8d18e2664e81d43c6836651d295d2[m[33m ([m[1;31morigin/fix-ubuntu-dotnet-install[m[33m)[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Oct 29 14:06:56 2019 +1000

    Follow redirect to download dotnet-install.sh

[33mcommit c0777758bed6030b6e7c62685f9c3896a5ecf72a[m
Merge: 43d681f1 cba400cf
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Oct 29 11:00:38 2019 +1000

    Merge pull request #1377 from MorganW09/dev
    
    Fixing up for grabs link [skip ci]

[33mcommit cba400cfcd0a550b77d56147dfeac4660a749508[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Oct 29 11:00:25 2019 +1000

    Note requirement for GitHub login [skip ci]

[33mcommit f27b961a28fd10965a0747c8a57186fdd4dbd6ef[m
Author: MorganW09 <morganwkenyon@gmail.com>
Date:   Fri Oct 25 19:16:21 2019 -0500

    Updating url to be cross repository

[33mcommit ba7db44b44ec2b322569345fa4814e89cfac463e[m
Author: MorganW09 <morganwkenyon@gmail.com>
Date:   Fri Oct 25 12:37:57 2019 -0500

    Fixing up for grabs link.

[33mcommit db18cbfcaa78835c8a3a338a273bef0529dec9ef[m
Author: Rafael Silveira Cordeiro <rafaelsc@rafaelsc.net>
Date:   Thu Oct 24 18:39:19 2019 -0700

    Add more tests to MessageTemplateParser

[33mcommit cdb160824f3e9ebfa48dc0f294da59c890d6a57d[m
Author: Rafael Silveira Cordeiro <rafaelsc@rafaelsc.net>
Date:   Mon Oct 21 12:55:46 2019 -0700

    Add more tests to MessageTemplateParser

[33mcommit b82d411ad33e2b58dbc9e443a1cdf2da9a226a6e[m
Author: Rafael Silveira Cordeiro <rafaelsc@rafaelsc.net>
Date:   Mon Oct 21 12:41:05 2019 -0700

    Add mote Tests to MessageTemplateParser

[33mcommit 43d681f1779d9448b5be4d5c47a69ef675898536[m
Merge: e092707b 8066ecff
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Oct 17 09:34:57 2019 +1000

    Merge pull request #1371 from perjahn/dev
    
    Documentation clarification.

[33mcommit 8066ecff50ae04858e2d4f0c124471d8b02eedc6[m
Author: Per Jahn <perjahn@gmail.com>
Date:   Wed Oct 16 23:55:27 2019 +0200

    Documentation clarification.

[33mcommit e092707b7114f4217de06cc3b18742b48a19bacb[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Oct 17 06:50:34 2019 +1000

    Update README link to the ASP.NET Core integration [skip ci]

[33mcommit d959fed6125101176ae47cabb4f04981511a9c69[m
Merge: 3007b0a5 060bd135
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Oct 15 16:00:46 2019 +1000

    Merge pull request #1370 from rafaelsc/improvements/unitTest/parser
    
    Improve Serilog.Parsing Unit Tests and Code Coverage

[33mcommit 060bd135c475703897318e0bc697d8b2e01d1d79[m
Author: Rafael Silveira Cordeiro <rafaelsc@rafaelsc.net>
Date:   Mon Oct 14 21:34:10 2019 -0700

    Better naming
    Refactor
    Fix Typos

[33mcommit a7d85d69532fec898d473fe7cd61bfef63290187[m
Author: Rafael Silveira Cordeiro <rafaelsc@rafaelsc.net>
Date:   Mon Oct 14 18:56:24 2019 -0700

    Improve Code Coverage

[33mcommit 0fa0c78e0b669c86a7d7685085e3be219b6d3861[m
Author: Rafael Silveira Cordeiro <rafaelsc@rafaelsc.net>
Date:   Mon Oct 14 18:19:53 2019 -0700

    Add more Parser Tests

[33mcommit 90cdec6c722f2b94c2485366ad4fc685ed7d16fb[m
Merge: ffc484a7 3007b0a5
Author: Rafael Sliveira Cordeiro <rafaelsc@rafaelsc.net>
Date:   Mon Oct 14 17:57:42 2019 -0700

    Merge pull request #4 from serilog/dev
    
    Merge last Serilog Code

[33mcommit 3007b0a535243d37090990a24d75c5124d704d21[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Oct 14 07:01:57 2019 +1000

    Dev version bump [skip ci]

[33mcommit 2b13493739628ee533d3456b34caab07fdbb1da2[m
Merge: 5c127cbe 3bb9e242
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Oct 5 18:59:04 2019 +1000

    Merge pull request #1366 from martinh2011/fix-custom-format-strings
    
    Allow `+` in message template property format specifiers

[33mcommit 3bb9e242aeac3f19fe7531fa5a6fe099ae7db2ed[m
Author: Martin Hüser <hueser@gmx.net>
Date:   Sat Oct 5 01:43:30 2019 +0200

    fix format strings with '+' not parsed correctly.
    
    fix #1353 Custom format string not accepted by the message
    template parser.
    
    Custom numeric format strings may contain plus '+' signs.  That
    character was not recognised by the parser.

[33mcommit ffcdba4d2052b04f54bc15084dd0a87417ad2835[m
Author: Martin Hüser <hueser@gmx.net>
Date:   Sat Oct 5 02:20:38 2019 +0200

    add unit test for #1353

[33mcommit 5c127cbebda73391eb69f2aa9c9170376a758826[m
Merge: f4c87982 0494601e
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Aug 31 11:37:53 2019 +1000

    Merge pull request #1357 from rob-earwaker/docs/default-destructuring-depth
    
    Update summary for default destructuring depth

[33mcommit ffc484a794390d3c99f8b37228357e3fdee6be18[m
Merge: d81f64fd f4c87982
Author: Rafael Sliveira Cordeiro <rafaelsc@rafaelsc.net>
Date:   Fri Aug 30 16:58:26 2019 -0700

    Merge pull request #3 from serilog/dev
    
    Merge Last Changes

[33mcommit 0494601e0b3fd238502ff1482a851e6a2e28134d[m
Author: Rob Earwaker <rob-earwaker@users.noreply.github.com>
Date:   Fri Aug 30 10:39:42 2019 +0100

    Update summary of ToMaximumDepth method

[33mcommit 1975e656a4191def3cee775343def313e56ff04b[m
Author: Rob Earwaker <rob-earwaker@users.noreply.github.com>
Date:   Fri Aug 30 10:33:46 2019 +0100

    Add test for default maximum destructuring depth

[33mcommit f4c879823ec722fc5614026728432a0eca457a11[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Jul 29 21:24:06 2019 +1000

    INFLUENCES.md (#1348)

[33mcommit 53aafc0170221b2600b36cedadcec5513395c74f[m
Merge: 69f8c14b d5c75fe3
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Jul 12 08:01:20 2019 +1000

    Merge pull request #1342 from WeihanLi/dev
    
    remove unnecessary System.Collections.NonGeneric dependency for netstandard2.0

[33mcommit d5c75fe35ce375791e99777a2338188bbee9687b[m
Author: WeihanLi <weihanli@outlook.com>
Date:   Wed Jul 10 16:40:47 2019 +0800

    remove unnecessary System.Collections.NonGeneric dependency for netstandard2.0

[33mcommit 69f8c14b6b8450b150a33e0bb04553a94d26cd23[m
Merge: e6936a15 fa1bfae0
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Jun 25 07:51:00 2019 +1000

    Merge pull request #1330 from bender2k14/#1328_syntatically_optional_IFormatProvider
    
    made formatProvider optional in the constructor of MessageTemplateTextFormatter

[33mcommit fa1bfae07fbe6d735f44d26eaac39746927d3f06[m
Author: Tyson Williams <tyson.williams@blocherconsulting.com>
Date:   Mon Jun 24 06:29:56 2019 -0500

    made formatProvider optional in the constructor of MessageTemplateTextFormatter #1328

[33mcommit e6936a15579d9c134a3fca680ff65f68d0d4f0f7[m
Merge: c2f78f01 d953cee4
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Jun 22 23:15:09 2019 +1000

    Merge pull request #1325 from nblumhardt/default-format
    
    Respect explicit format specifiers even when JSON defaults are selected

[33mcommit c2f78f01c04345b1c1ea25d3111983f63f00affd[m
Merge: 8ed3afd0 fde1a321
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Jun 21 18:39:09 2019 +1000

    Merge pull request #1320 from teo-tsirpanis/dev
    
    Use snupkg symbol packages.

[33mcommit d953cee4b14c86a570aaa2423587a12bf622c389[m[33m ([m[1;31morigin/default-format[m[33m)[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Jun 21 03:19:39 2019 +1000

    Fixes #1323 - respect explicit format specifiers even when JSON defaults are selected

[33mcommit fde1a3211c71f4dbf8cbb09b679fed2497ae5515[m
Author: Theodore Tsirpanis <teo.tsirpanis.718@gmail.com>
Date:   Sat Jun 15 01:25:58 2019 +0300

    Fix AppVeyor artifact paths.

[33mcommit 177ecd2b4b0e8cbe6eb2009d8a799e8a6dbff962[m
Author: Theodore Tsirpanis <teo.tsirpanis.718@gmail.com>
Date:   Sat Jun 15 01:12:26 2019 +0300

    Use the actual project's site in the package metadata.

[33mcommit 57006a19bd63baf0a81456bb4c880ec82a20638d[m
Author: Theodore Tsirpanis <teo.tsirpanis.718@gmail.com>
Date:   Sat Jun 15 00:54:56 2019 +0300

    Use SourceLink and separate symbol packages.

[33mcommit 8ed3afd0f2d942429832b6674a2f599c7fb1fa38[m
Merge: 70624912 c8702094
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Jun 3 12:46:26 2019 +1000

    Merge pull request #1316 from nblumhardt/wrapped-enrichers
    
    Wrapped enrichers, conditional/leveled enrichers, conditional sinks

[33mcommit 70624912bf79cf0406516d57c0fc225d783d281f[m
Merge: 36c39eb8 c5233b2f
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri May 31 11:49:02 2019 +1000

    Merge pull request #1317 from SimonCropp/useVar
    
    use var for out

[33mcommit c5233b2f61857be8100334b41be56e9201b3552c[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Fri May 31 11:14:52 2019 +1000

    redundant usings due to var change

[33mcommit d39df5803fd3c145731be5579ac6023ea13b3eaa[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Fri May 31 11:12:31 2019 +1000

    use var for out

[33mcommit c8702094e95f3312a384e0f50c4e68483ad3d1ca[m[33m ([m[1;31morigin/wrapped-enrichers[m[33m)[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed May 29 17:11:09 2019 +1000

    WriteTo.Conditional()

[33mcommit 2b70d8ee0fa363dbd89a3c74a58e059001d30217[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed May 29 16:40:00 2019 +1000

    Enrich.When() and Enrich.AtLevel()

[33mcommit ac5400b82a858e47bbc6e33b28d729a556f7372f[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed May 29 16:13:50 2019 +1000

    LoggerEnrichmentConfiguration.Wrap()

[33mcommit 36c39eb882533247e1880bdb376ce6a7cca01802[m
Merge: a1e9850d fa506c80
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed May 29 16:05:06 2019 +1000

    Merge pull request #1266 from tsimbalar/nuget-license
    
    Update Nuget package license format to new format

[33mcommit a1e9850db854cc43b2d2fff29eeb38cb1a93341b[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed May 29 12:08:37 2019 +1000

    Struct alternative to LogEventProperty; optimizations (#1315)
    
    Includes a .NET SDK and AppVeyor build image update.

[33mcommit d81f64fd001ed58a44b227be92e02734b2cd29e4[m
Merge: dd40ea56 decfe093
Author: Rafael Sliveira Cordeiro <rafaelsc@rafaelsc.net>
Date:   Sat May 25 15:07:02 2019 -0700

    Merge pull request #2 from serilog/dev
    
    Merge last Serilog Changes

[33mcommit decfe093b48a152364df30322fc8532996e3e60b[m
Merge: 8ce1bcec 7ba406ab
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri May 24 11:11:11 2019 +1000

    Merge pull request #1314 from SimonCropp/minorCleanup
    
    Some very minor cleanup

[33mcommit 7ba406abb95b7fb791e6f244e05fed694a7d3620[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Fri May 24 09:57:42 2019 +1000

    use static where possible
    
    reduces stack alloc

[33mcommit a43d55dcb194c8eb93b57bf614396a052b13e1f6[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Fri May 24 09:46:00 2019 +1000

    remove some duplicate type checking

[33mcommit bf9061c44116614c91dccc755b03768eae26fcac[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Fri May 24 09:34:30 2019 +1000

    simpler usings

[33mcommit 06334a2452272f7fdc5b7d538d108cc2564d4791[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Fri May 24 09:32:21 2019 +1000

    spelling

[33mcommit fa506c806d9c8a4e662bcea8b74b9a12ee7c63e6[m
Merge: 06ade796 8ce1bcec
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon May 20 08:34:02 2019 +1000

    Merge branch 'dev' into nuget-license

[33mcommit 8ce1bcec9d7a82c4e7990927470d7e3948bfdafe[m
Merge: 4dc6eb7c 9d777c44
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon May 20 08:32:36 2019 +1000

    Merge pull request #1312 from sungam3r/code-cleanup
    
    Code cleanup

[33mcommit 9d777c447a84f8bf32f6f569daef10eb0c10554d[m
Author: Ivan Maximov <sungam3r@yandex.ru>
Date:   Fri May 17 02:02:35 2019 +0300

    revert to select

[33mcommit d3be73f6e2562db54e0443319d49914ebeb1e031[m
Author: Ivan Maximov <sungam3r@yandex.ru>
Date:   Fri May 17 01:38:56 2019 +0300

    return pragma

[33mcommit 9eb26f437e9d24fe9d9be7aad81195d34d2383be[m
Author: Ivan Maximov <sungam3r@yandex.ru>
Date:   Fri May 17 00:19:31 2019 +0300

    formatting

[33mcommit 0826ad9c183aa00760919b304a17277cdf0550ba[m
Author: Ivan Maximov <sungam3r@yandex.ru>
Date:   Thu May 16 02:51:38 2019 +0300

    throw expressions

[33mcommit 1246d8962150219425d2e60e11fa1de3db3f3a16[m
Author: Ivan Maximov <sungam3r@yandex.ru>
Date:   Thu May 16 02:44:33 2019 +0300

    LangVersion to latest

[33mcommit 0a36e67e575d0e911a3eee5b4f2438836e6d58a6[m
Author: Ivan Maximov <sungam3r@yandex.ru>
Date:   Thu May 16 02:44:08 2019 +0300

    pattern mathing

[33mcommit b4088c413b7b502f585fb282066827e80184bea3[m
Author: Ivan Maximov <sungam3r@yandex.ru>
Date:   Thu May 16 02:27:15 2019 +0300

    variable inlining

[33mcommit ccd6903d54d08a09e8455dcfd639d59938c5c331[m
Author: Ivan Maximov <sungam3r@yandex.ru>
Date:   Thu May 16 02:20:11 2019 +0300

    typos

[33mcommit 4dc6eb7c8eda1ace1233eb3985daef9b1e1a4be8[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Fri May 3 07:43:22 2019 +1000

    Corrected NuGet API Key

[33mcommit e2169cf0abe79838f4082cfa3c50ed930702de26[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Fri May 3 07:34:23 2019 +1000

    Update NuGet API Key

[33mcommit 77e9e28b3d8a965516a8407839a74abdfb33800e[m
Merge: ec87800f 7976970c
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Apr 30 09:59:27 2019 +1000

    Merge pull request #1300 from dahlsailrunner/ps-readme-addition
    
    Added link to search of Pluralsight courses on readme [skip ci]

[33mcommit 7976970c3147ca4b8b5e69d8ef5cf44a7b914677[m
Author: Erik Dahl <dahlsailrunner@yahoo.com>
Date:   Mon Apr 29 06:53:28 2019 -0500

    updated ps link to www for non-logged in users

[33mcommit f4947251f58a4193132e8e15fc6464f7f628a95a[m
Author: Erik Dahl <dahlsailrunner@yahoo.com>
Date:   Sun Apr 28 06:45:58 2019 -0500

    added link to search of ps courses on readme

[33mcommit ec87800f0375ca8c58285987e950703310a3be65[m
Merge: ca9664f3 e61ffbf8
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Apr 26 18:12:45 2019 +1000

    Merge pull request #1255 from Pliner/immutable-stack-enumerator
    
    Handwritten implementation of IEnumerator to avoid allocations

[33mcommit ca9664f3091815a449d62fd2811b7f755d281e53[m
Merge: cc0bdba7 3ab95da8
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Apr 26 18:02:08 2019 +1000

    Merge pull request #1254 from Pliner/log_event_level_boxing
    
    Reduce LogEventLevel boxing

[33mcommit 3ab95da823c06d47adef9641e4dca553f7a1e58f[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Apr 26 17:44:53 2019 +1000

    Trivial whitespace change [skip ci]

[33mcommit cc0bdba7684ad09d19ad69950626da6be940bcf0[m
Merge: e6389415 4561e408
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Apr 22 13:23:24 2019 +1000

    Merge pull request #1296 from merbla/remove-travisci
    
    Replace TravisCI with AppVeyor for Linux builds

[33mcommit 4561e4087ed9d59719c3f88de313d5f964729a64[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Thu Apr 4 19:31:20 2019 +1000

    Removed README notes on TravisCI.

[33mcommit 695ffaba1a512b04f2f73cc2be52c0787d1a99e5[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Thu Apr 4 19:15:08 2019 +1000

    Removed TravisCI and replaced with AppVeyor for Linux builds.

[33mcommit dd40ea569046d0bc4ddd101392abfedb4cbcc330[m
Merge: 5c3a7821 e6389415
Author: Rafael Sliveira Cordeiro <rafaelsc@rafaelsc.net>
Date:   Tue Feb 26 23:02:54 2019 -0800

    Merge pull request #1 from serilog/dev
    
    Merge Last Serilog Changes

[33mcommit e638941551d353dbc588f0f21ab397d5d259de43[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Jan 17 09:25:02 2019 +1000

    Dev version bump [skip ci]

[33mcommit f1972698c3912a8ded1f72829026608400cd66bc[m
Merge: 9fd976ac b016a053
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Jan 14 11:05:52 2019 +1000

    Merge pull request #1269 from nblumhardt/netstandard2.0
    
    Add .NET Standard 2.0 target

[33mcommit 9fd976acad374cf90efc54f285fcf356764cb5ba[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Jan 10 12:38:06 2019 +1000

    Quick build fix (oops!)

[33mcommit 99a095b3ac602c2d2ef27d7f3a1544955cdb6ba3[m
Merge: 5c3a7821 1d8a8aa6
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Jan 10 11:45:44 2019 +1000

    Merge branch 'balayoglu-dev' into dev

[33mcommit 1d8a8aa6d5115f357d9a4897cae86591fdbe7cb8[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Jan 10 11:45:31 2019 +1000

    Minor tweaks

[33mcommit b016a053096d13b061166a2b83c7d00bfefb8ee6[m[33m ([m[1;31morigin/netstandard2.0[m[33m)[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Jan 10 10:36:12 2019 +1000

    Add .NET Standard 2.0 target; bump version to 2.8; fixes #1268

[33mcommit 06ade796387d25f6b5267b72cc538ab5a2bb6259[m
Author: Thibaud Desodt <tibo.desodt@gmail.com>
Date:   Fri Jan 4 19:23:39 2019 +0100

    Update Nuget package license format to new format
    
    `licenseUrl` is now deprecated, so we use `license expression` instead

[33mcommit e61ffbf84239a540a90e0509d95bb2c2598db134[m
Author: Yury Pliner <yury.pliner@gmail.com>
Date:   Mon Dec 10 21:18:44 2018 +0500

    Handwritten implementation of IEnumerator  to avoid allocations

[33mcommit d239d7ce1cbdf367067c2105e58f71d2e9d21ed1[m
Author: Yury Pliner <yury.pliner@gmail.com>
Date:   Mon Dec 10 17:47:08 2018 +0500

    Reduce LogEventLevel boxing

[33mcommit 4e8571e3fea28f6e26197fe7df855f68b5380d79[m
Author: Nazim Balayoglu <nazim.balayev@allenovery.com>
Date:   Wed Oct 17 16:22:24 2018 +0100

    Fix on GetPropertiesRecursive method not to generate exception on the case that BaseType of the argument Type is null

[33mcommit 5c3a7821aa0f654e551dc21e8e19089f6767666b[m
Merge: 15268374 be37f1c9
Author: Thibaud Desodt <tibo.desodt@gmail.com>
Date:   Mon Oct 15 09:38:32 2018 +0200

    Merge pull request #1234 from tsimbalar/custom-formatter-tests
    
    Add tests around support of custom `IFormatProvider`

[33mcommit 15268374a6662f22688fb6f906de97fbc512d1e0[m
Merge: 12cdca02 e132a451
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Oct 15 10:11:31 2018 +1000

    Merge pull request #1169 from merbla/netcoreapp2-perf-testing
    
    Update Performance Tests to NetCoreApp2

[33mcommit be37f1c9782c830f3bed3fdaf0ddcb54bb69173a[m
Merge: 6a60eb19 12cdca02
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Oct 15 08:02:36 2018 +1000

    Merge branch 'dev' into custom-formatter-tests

[33mcommit 12cdca02050cfc48b771ab637a92612fd56d8914[m
Merge: 5b0569ac cfaef7a8
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Oct 15 08:00:46 2018 +1000

    Merge pull request #1233 from nblumhardt/padding
    
    Fix output template padding of non-String values

[33mcommit 6a60eb195f43debf70a9e4a9a9da8340980b9758[m
Author: Thibaud Desodt <tibo.desodt@gmail.com>
Date:   Sat Oct 13 14:59:03 2018 +0200

    Add tests around support of custom `IFormatProvider`

[33mcommit cfaef7a88c6572255a5478d23a8bd9c85ad0a33e[m[33m ([m[1;31morigin/padding[m[33m)[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Oct 13 06:45:52 2018 +1000

    Fixes serilog/serilog-sinks-rollingfile#71, padding in output templates not correctly applied to non-String properties

[33mcommit 5b0569ac42363dffced9854b0406ca338248f5fc[m
Merge: 6dd2ddbd 55a27a96
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Sep 22 10:11:27 2018 +1000

    Merge pull request #1220 from MaximRouiller/repositoryurl
    
    Include Repository URL is NUPKG

[33mcommit 55a27a962c6d2a13e2fb7a627acfddf0612dc49c[m
Author: Maxime Rouiller <angelzerosoft@gmail.com>
Date:   Fri Sep 21 10:16:53 2018 -0400

    fixing repository url

[33mcommit 6dd2ddbd6d5c7e37f4efd76eaa5b99fb3ee2770e[m
Merge: 9509f4a8 9eb02b79
Author: Thibaud Desodt <tibo.desodt@gmail.com>
Date:   Wed Aug 1 08:37:46 2018 +0200

    Merge pull request #1198 from tsimbalar/config-method-support
    
    Add support for more configuration methods from KeyValueSettings
    
    - `WriteTo.Sink(ILogEventSink logEventSink, LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum, LoggingLevelSwitch levelSwitch = null)`
    - `AuditTo.Sink(ILogEventSink logEventSink, LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum, LoggingLevelSwitch levelSwitch = null)`
    - `Enrich.With(params ILogEventEnricher[] enrichers)` (with surrogate `Enrich.With(ILogEventEnricher enricher)` )
    - `Filter.With(params ILogEventFilter[] filters)` (with surrogate `Enrich.With(ILogEventFilter filter)` )

[33mcommit 9509f4a81769f6cb20a0a6b196d6a24bcc2b4d97[m
Merge: fcd0c8bb 12505ebc
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Jul 31 20:30:17 2018 +1000

    Merge pull request #1197 from ie-zero/Logger.None
    
    Allow SilentLogger to be contructed as singleton

[33mcommit 12505ebc1a9d37b8cf7ab64e2c92f61733ba21c0[m
Author: I. Evangelinos <ievangelinos@outlook.com>
Date:   Mon Jul 30 20:57:06 2018 +0100

    Enforce the creation of SillentLogger as singleton

[33mcommit 4da0020386fe1b6a1e126e54030447d88802b1f7[m
Author: I. Evangelinos <ie-zero@users.noreply.github.com>
Date:   Sun Jul 29 19:32:18 2018 +0100

    Minor improvement on SilentLogger
    
    * Rename None property on SilentLogger to convey the better the usage.
    * Reorder the modifiers of Instance property on SilentLogger to align with the coding guidlines

[33mcommit 1b0e3674ae869368dc2ed9e157b9ac04e461da70[m
Author: I. Evangelinos <ie-zero@users.noreply.github.com>
Date:   Sat Jul 28 16:41:25 2018 +0100

    Allow SilentLogger to be contructed as singleton
    
    Fix #1195: This allows comparision Log.Logger = Logger.None

[33mcommit 9eb02b79a6b8106d4ef2b3c55d17e081df382064[m
Author: Thibaud Desodt <tibo.desodt@gmail.com>
Date:   Fri Jul 27 18:34:46 2018 +0200

    Add support for Filter.With() in KeyValueSettings
    
     Support for `Filter.With(ILogEventFilter filter)`

[33mcommit c31ecb721bfb2dbb17c10c7a4cad37ebf2c84e78[m
Author: Thibaud Desodt <tibo.desodt@gmail.com>
Date:   Fri Jul 27 08:32:10 2018 +0200

    Add support for Enrich.With() in KeyValueSettings
    
     Support for `Enrich.With(ILogEventEnricher enricher)`

[33mcommit a5efb0da7289972c97663ed817c168c90af5aa1a[m
Author: Thibaud Desodt <tibo.desodt@gmail.com>
Date:   Fri Jul 27 08:12:49 2018 +0200

    Add support for Audit.Sink() in KeyValueSettings
    
     Support for `AuditTo.Sink(ILogEventSink logEventSink, LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum, LoggingLevelSwitch levelSwitch = null)`

[33mcommit 0540048f5feded3cd6ef627708dec432592174cb[m
Author: Thibaud Desodt <tibo.desodt@gmail.com>
Date:   Thu Jul 26 07:53:01 2018 +0200

    Add support for WriteTo.Sink() in KeyValueSettings
    
    Support for `WriteTo.Sink(ILogEventSink logEventSink, LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum, LoggingLevelSwitch levelSwitch = null)`

[33mcommit fcd0c8bb128a853b48869dd7821b3e8661963319[m
Merge: 163e51f7 088ca6aa
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Jul 18 07:21:18 2018 +1000

    Merge pull request #1192 from tsimbalar/destructure-with
    
    Add support for Destructure.With(IDestructuringPolicy policy) in settings

[33mcommit 088ca6aaa78871337da936898cb084c9bfb98708[m
Author: Thibaud Desodt <tibo.desodt@gmail.com>
Date:   Tue Jul 17 18:14:35 2018 +0200

    Add support for Destructure.With(IDestructuringPolicy policy) in settings
    
    The key-value settings will accept the assembly-qualified name of a type implementing IDestructuringPolicy if it has a default constructor

[33mcommit 163e51f76ea925c21408d80a61dd3d8ea45490cd[m
Merge: 6b5aa000 22d05fd2
Author: Thibaud Desodt <tibo.desodt@gmail.com>
Date:   Mon Jul 9 08:59:29 2018 +0200

    Merge pull request #1189 from skomis-mm/uwpCertRenew
    
    Renew certificate for UWP test project

[33mcommit 22d05fd211238a02d6b03f2153b29159db5ffbb6[m
Author: Sergey Komisarchik <komisarchik@media-saturn.com>
Date:   Mon Jul 9 09:34:01 2018 +0300

    renew cert for uwp signing

[33mcommit 6b5aa0003614df057346e506ecc0dc3a94e97fdd[m
Merge: 9a46e68d d643cf5d
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Jun 30 07:22:10 2018 +1000

    Merge pull request #1179 from tsimbalar/destructure-asscalar
    
    Support for .Destructure.AsScalar(scalarType) in KeyValueSettings

[33mcommit d643cf5dddea7d67a2681caf552ba5f415e57a9f[m
Author: Thibaud Desodt <tibo.desodt@gmail.com>
Date:   Thu Jun 14 09:37:06 2018 +0200

    Fix test failing because of wrong assembly qualified type names

[33mcommit 3ccecf15fabb13db3adc9f6c1a468c08f77d6631[m
Author: Thibaud Desodt <tibo.desodt@gmail.com>
Date:   Wed Jun 13 19:57:41 2018 +0200

    Support for .Destructure.AsScalar(scalarType) in KeyValueSettings

[33mcommit 9a46e68d3e224b965f4ac2665fd379ae9568d6b4[m
Merge: ed29c30d dac5190a
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Jun 13 17:11:41 2018 +1000

    Merge pull request #1174 from tsimbalar/kvp-destructure
    
    Add partial support for .Destructure.XXX() methods in `KeyValuePairSettings`

[33mcommit dac5190a69e10e19e9e834357d62f79e854f375c[m
Author: Thibaud Desodt <tibo.desodt@gmail.com>
Date:   Tue Jun 12 21:40:24 2018 +0200

    Move "surrogates" to a dedicated file

[33mcommit b412c6c78e0cd72b5e55ba0aea094124ad07f822[m
Author: Thibaud Desodt <tibo.desodt@gmail.com>
Date:   Tue Jun 12 21:26:33 2018 +0200

    Renamed Surrogate methods properly

[33mcommit b5db2f91b1a01fe9d973794ce6af052ae0bd558b[m
Author: Thibaud Desodt <tibo.desodt@gmail.com>
Date:   Thu Jun 7 19:32:45 2018 +0200

    Add support for .Destructure.XXX() methods
    
    Supports Serilog "native" non-generic simple  methods :
    - `ToMaximumCollectionCount`
    - `ToMaximumDepth`
    - `ToMaximumStringLength`
    
    and any *extension method* for `LoggerDestructuringConfiguration`
    
    This should fix #1172

[33mcommit ed29c30d794ee7627726870fa25079f2a75a0f3e[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri May 18 19:43:13 2018 +1000

    Dev version bump [Skip CI]

[33mcommit b481e550a79e10b445e164babc1333c0b4271093[m
Author: Serilog Build <build@serilog.net>
Date:   Fri May 18 18:56:00 2018 +1000

    Updated CHANGES.md with 2.6.0 and 2.7.1

[33mcommit e132a451cb2d4318adc2c427577e875b9434fbea[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Fri May 18 13:57:36 2018 +1000

    Update perf tests to netcoreapp2 with output.

[33mcommit fd0e44059c664aa949c0dd27c00b6947bf29fd25[m
Merge: d0ea61ff ee442143
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon May 14 08:25:27 2018 +1000

    Merge pull request #1165 from nblumhardt/closed-wrap
    
    Fixes a number of issues with `LoggerSinkConfiguration.Wrap()`

[33mcommit ee442143eb4c45081671a2b09c96c25c96a4ef5d[m[33m ([m[1;31morigin/closed-wrap[m[33m)[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon May 14 08:08:07 2018 +1000

    Use Count rather than Any() for consistency

[33mcommit d0ea61ff68ecbc7ca32b90bdad381c5428537314[m
Merge: c039f48c 1ab4e2ca
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon May 14 08:04:03 2018 +1000

    Merge pull request #1166 from skomis-mm/configRef
    
    Created Logger should not hold reference to its configuration

[33mcommit 1ab4e2caca8df21e794b32563b7aefde6222fd9a[m
Author: Sergey Komisarchik <komisarchik@media-saturn.com>
Date:   Sun May 13 20:56:03 2018 +0300

    use array withing foreach loop when disposing sinks

[33mcommit dedf01e01bad8561ac04de755bc8a0794e776e1e[m
Author: Sergey Komisarchik <komisarchik@media-saturn.com>
Date:   Sun May 13 17:51:02 2018 +0300

    Created Logger should not hold reference to its configuration

[33mcommit 054962947e045d417e65ac06b4755f94dca86501[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun May 13 19:20:23 2018 +1000

    Fixes a number of issues with `LoggerSinkConfiguration.Wrap()` identified in #1154
    
     - Ensures that `wt.X(); wt.Y()` and `wt.X().WriteTo.Y()` are equivalent
     - Ensures a failing sink doesn't block events from reaching others inside the same wrapper, much as using a secondary logger would do
     - Prevents chained calls from accidentally breaking thread-safety rules by turning `Enrich` inside wrappers into a no-op

[33mcommit c039f48cffb8e83eb96069d2e42cebc5af4bcc20[m
Merge: 8514bd84 23586dc6
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun May 13 19:15:15 2018 +1000

    Merge pull request #1157 from adamchester/update-gitignore
    
    Update gitignore

[33mcommit 8514bd8401f55c1acd676d458ab6644a3b81b0a5[m
Merge: 10fe64d6 3ed1f90e
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sat May 12 14:47:28 2018 +1000

    Merge pull request #1163 from skomis-mm/srclnk
    
    SourceLink v2 support added

[33mcommit 3ed1f90ea4a1496d5656cb9f9645a787fda0bbf8[m
Author: Sergey Komisarchik <komisarchik@media-saturn.com>
Date:   Fri May 11 15:18:27 2018 +0300

    SourceLink support added

[33mcommit 10fe64d616f768103e34d0c2ec519cf66861510f[m
Merge: d64ab089 a1c7ccfe
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue May 8 13:02:07 2018 +1000

    Merge pull request #1160 from merbla/new-nuget-api-key
    
    Updated NuGet API Key.

[33mcommit a1c7ccfedf051cdc96ccf26c9e38b19e81aa53ed[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Tue May 8 11:00:28 2018 +1000

    Updated NuGet API Key.

[33mcommit d64ab089751af7702bffcfe331ed1d55d8c4f5dd[m
Merge: 11cd43a1 a6e3c969
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Tue May 8 09:25:42 2018 +1000

    Merge pull request #1158 from adamchester/re-enable-osx-travis
    
    Enable OSX builds on travis again

[33mcommit a6e3c96911a51767a1a47a3873cdc54a83407220[m
Author: Adam Chester <adamchester@gmail.com>
Date:   Mon May 7 13:32:39 2018 +1000

    Enable OSX builds on travis again

[33mcommit 23586dc6b74cfac287a77d64ef993a5d46fa6295[m
Author: Adam Chester <adamchester@gmail.com>
Date:   Mon May 7 13:16:43 2018 +1000

    Ignore `.vscode`

[33mcommit 4a94653b0b6d95b46eda5e9ae2ffeb31e0c95fbd[m
Author: Adam Chester <adamchester@gmail.com>
Date:   Mon May 7 13:16:19 2018 +1000

    Updated .gitignore,
    
    from https://github.com/github/gitignore/blob/18e28746b0862059dbee8694fd366a679cb812fb/VisualStudio.gitignore

[33mcommit 9cc03abe0088ffe26ce1265d47d87be9f8c85666[m
Author: Adam Chester <adamchester@gmail.com>
Date:   Sun May 6 11:39:06 2018 +1000

    update .gitignore from https://www.gitignore.io/api/visualstudio

[33mcommit 11cd43a18c1254a833647763131e3404aa066d1a[m
Merge: 4331b6ba 1b0de533
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Mar 24 10:17:37 2018 +1000

    Merge pull request #1141 from Pliner/dev
    
    Fix #1133

[33mcommit 1b0de533db5696369969841bdee9bc0a56f88073[m
Author: yupliner <yupliner@skbkontur.ru>
Date:   Fri Mar 23 10:12:24 2018 +0500

    Fix #1133

[33mcommit 4331b6baf5c8824b33176be007b726ea0832bfe0[m
Merge: 1d8cebf0 80c47028
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Wed Mar 21 11:01:48 2018 +1000

    Merge pull request #1137 from merbla/bugfix-bash-exit-on-error
    
    Fix OSX/*nix build #1136

[33mcommit 80c47028e7fcd517c649170cfbae5dc78057e565[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Wed Mar 21 10:38:18 2018 +1000

    Updated Travis build to dotnet 2.1.4

[33mcommit 4a4ff20afe555ea399bdc5926aaf5e2326334640[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Wed Mar 21 09:22:28 2018 +1000

    Target netcoreapp2 for tests.

[33mcommit 95773188832db2021ecbc22ed8eda62eedee5ed0[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Wed Mar 21 09:19:09 2018 +1000

    Fixes #1136. Set exit on error.

[33mcommit 1d8cebf03f41046cd237af22be65df4a357eba7e[m
Merge: 031872f5 80229af4
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Mar 20 16:13:14 2018 +1000

    Merge pull request #1134 from merbla/nuget-badges
    
    Update of README badges to include downloads and tests. [Skip CI]

[33mcommit 80229af4100cb33a67fb9f727a9174968b15e193[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Tue Mar 20 14:07:27 2018 +1000

    Update of badges to include downloads and tests.

[33mcommit 031872f5b369a9eddbcc6bdd7e3f8aa2315f5dde[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Mar 5 08:29:06 2018 +1000

    It's 2018! [Skip CI]

[33mcommit 7629de673fdd32ae2fad4dfe53620e06555a064e[m
Merge: 96bfe61c e83b8a72
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Feb 22 08:23:57 2018 +1000

    Merge pull request #1113 from skomis-mm/contexttestfix
    
    `ContextPropertiesCrossAsyncCalls` test failure fix on CI:

[33mcommit e83b8a72e101c0bafe532c6f61324068de0e2505[m
Author: Sergey Komisarchik <komisarchik@media-saturn.com>
Date:   Wed Feb 21 12:11:13 2018 +0300

    TestWithSyncContext fix

[33mcommit 43e095d9f949155f0597553fb727e4d33f09b716[m
Author: Sergey Komisarchik <komisarchik@media-saturn.com>
Date:   Wed Feb 21 12:04:21 2018 +0300

    `ContextPropertiesCrossAsyncCalls` test failure fix on CI:
    - force schedule async continuations on a new thread

[33mcommit 96bfe61c7e35271450062846374c13cce75b83e5[m
Merge: eca10067 cb5514bd
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Feb 21 07:52:58 2018 +1000

    Merge pull request #1110 from IanYates/issue-1105-b
    
    Fixes #1105 - add a static None property to the Logger class

[33mcommit cb5514bdd63c3cd47a36d2ffa11c3f533e3e249d[m
Author: IanYates <ian@medicalit.com.au>
Date:   Wed Feb 21 00:09:47 2018 +1000

    Additional test to verify Logger.None does not change between invocations

[33mcommit 12ff89687a5f850f2d6c460050a32373a528c1ad[m
Author: IanYates <ian@medicalit.com.au>
Date:   Wed Feb 21 00:04:40 2018 +1000

    Addressed PR feedback

[33mcommit 3e1a1c473fa77739ba319690c6998cd2a9b0d035[m
Author: IanYates <ian@medicalit.com.au>
Date:   Mon Feb 19 19:24:08 2018 +1000

    Fixes #1105 - add a static None property to the Logger class, exposing a constant SilentLogger instance

[33mcommit eca10067ce5126a19db2ef4b68d5e7791732a813[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Feb 16 15:33:40 2018 +1000

    Brought README examples up-to-date [Skip CI]

[33mcommit 2d4d8d13cffe33e5c72e6c6f4a29f84aaba8f5a3[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Jan 28 12:18:17 2018 +1000

    New method overload and formatting behavior changes require minor version bump

[33mcommit f6ce668b7ba35bbb902abfde10f1da8f63a70d4c[m
Merge: 76382297 5dfd146c
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Jan 28 11:52:24 2018 +1000

    Merge pull request #1091 from nblumhardt/new-properties-formatting
    
    Improvements to {Properties} handling in output templates

[33mcommit 5dfd146ce41d2a779d018671f0cd3ab37170a215[m[33m ([m[1;31morigin/new-properties-formatting[m[33m)[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Jan 24 08:11:04 2018 +1000

    Deal with intermittent test failure that appears to be due to the test adapter upgrade

[33mcommit 6c9fd16c764506517b1084e6028be4bc5454cf07[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Jan 23 21:48:36 2018 +1000

    Last round of test fixes :-)

[33mcommit 967bc94e6df9f0bc7b6cefe31b4e65ffdc82b5ea[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Jan 23 21:31:12 2018 +1000

    Update empty properties formatting to match structure default, take 2

[33mcommit 745a4769287a8861aaf34f9ff9f4c4a12452a46d[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Jan 23 21:26:05 2018 +1000

    Update empty properties formatting to match structure default

[33mcommit c8a14a8a1d0213996eacac62ab3f78594094b57b[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Jan 23 21:09:07 2018 +1000

    Straighten out some redundant code

[33mcommit 2066cb4a7560b11e5e0e298c0c10d5fac41adf27[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Jan 23 21:06:30 2018 +1000

    Update xUnit, apply all the helpful suggestions

[33mcommit 6a02f42aca2f423592daa3eeac8cee0a1233fd66[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Jan 23 20:30:21 2018 +1000

    Modernize the CSPROJ/SDK, fix some warnings, fix build

[33mcommit 32759d3f949b959adc523ed248e6bc4a13961616[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Jan 23 20:04:42 2018 +1000

    Additional test

[33mcommit c41c8231b7f3e510b2680a4f0c4f6c5d52e3f44d[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Jan 23 17:57:02 2018 +1000

    Fixes serilog/serilog-sinks-file#45 - support {Properties:j} in output templates; fixes #1088 - handle positional properties when computing {Properties}

[33mcommit 76382297c56deda36f259c5f889150e3da9fd50b[m
Merge: ba4c0e9c 393d44be
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Jan 22 13:24:11 2018 +1000

    Merge pull request #1083 from nblumhardt/https-package-links
    
    Update package metadata URLs

[33mcommit 393d44beab451e9f16f4fc40ced215bf69a47864[m[33m ([m[1;31morigin/https-package-links[m[33m)[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Jan 15 07:54:25 2018 +1000

    Update package metadata URLs - use HTTPS, point the project URL to GitHub

[33mcommit ba4c0e9c105c0d3c884c540cf6ee5e6fde7bcf61[m
Merge: 9e688f6b c0bd6428
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Dec 30 07:08:54 2017 +1000

    Merge pull request #1075 from joostas/dev-1073
    
    Closes #1073 - Overload Wrap() to accept LogEventLevel and LoggingLevelSwitch.

[33mcommit c0bd642853d7cf1da0caee848a8c97e84d8c23de[m
Author: joostas <justas.narbutas@gmail.com>
Date:   Fri Dec 29 20:55:56 2017 +0200

    Overload Wrap() to accept LogEventLevel and LoggingLevelSwitch.

[33mcommit 9e688f6b7715d064031c678d60f4f53b19a28bd3[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Dec 4 14:13:40 2017 +1000

    Dev version bump [Skip CI]

[33mcommit c5e719f75955e0a48b1c92897aae78fdfd31dd35[m
Merge: f284f85c f467dbba
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Nov 27 16:25:28 2017 +1000

    Merge pull request #1068 from nikolaybobrovskiy/patch-1
    
    Fixes #1067

[33mcommit f467dbbaded5ddf5b059b9b000096363e3287e0f[m
Author: Nikolay Bobrovskiy <nikolay.bobrovsky@gmail.com>
Date:   Mon Nov 27 03:20:44 2017 +0300

    Fixes #1067

[33mcommit f284f85ce585fe59c26fc349df3043c90fb542b8[m
Merge: f0d5d29a e91e3777
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Nov 24 07:23:04 2017 +1000

    Merge pull request #1064 from tsimbalar/settings-static-member
    
    Add support for reference to public static properties in settings

[33mcommit e91e377753bdd873b82defc2879d25ab4f8ad448[m
Author: Thibaud DESODT <thibaud.desodt@dgtresor.gouv.fr>
Date:   Thu Nov 23 07:14:12 2017 +0100

    Fix wrong class hierarchy in Abstract class test cases

[33mcommit dde160df0cefd3713d3bb2e3212c09e65cb4d56b[m
Author: Thibaud DESODT <thibaud.desodt@dgtresor.gouv.fr>
Date:   Thu Nov 23 07:05:30 2017 +0100

    Only support static member accessors for interfaces and abstract types

[33mcommit e81439ae1b5661cad1b099c361d8230a7bda56e9[m
Author: Thibaud DESODT <thibaud.desodt@dgtresor.gouv.fr>
Date:   Tue Nov 21 17:51:04 2017 +0100

    Extend the class::static handling to also support public static fields

[33mcommit a20dce5651d3fada10b67e366b7676b8818c8531[m
Author: Thibaud DESODT <thibaud.desodt@dgtresor.gouv.fr>
Date:   Fri Nov 17 07:26:38 2017 +0100

    Add support for reference to public static properties in settings
    
    as a value for parameters, using the syntax `NameSpace.To.ConcreteType::StaticProperty, AssemblyName`

[33mcommit f0d5d29a28df6eba46c5f9aa4e8a17dae8e97a74[m
Merge: 4806a0b6 badd605b
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Nov 16 07:27:26 2017 +1000

    Merge pull request #1063 from pardahlman/feature/adjust-namespace
    
    Move LoggerExtension so that namespace and folder matches

[33mcommit badd605b3b145cfdac1dbd5c4c0b63987b1e688b[m
Author: Pär Dahlman <par.dahlman@fellowdeveloper.se>
Date:   Wed Nov 15 19:40:25 2017 +0100

    Move LoggerExtension so that namespace and folder matches

[33mcommit 4806a0b647e2f7cece64c00496dc7c79ba371e68[m
Merge: 6116df04 64e45ab1
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Nov 10 15:46:22 2017 +1000

    Merge pull request #1051 from tsimbalar/kvpsettings-readonlydict
    
    Handle duplicate key-value pair settings - last-in wins

[33mcommit 6116df0425b5619f7fa260e7534fd630bfec3263[m
Merge: 9f273756 f19f1968
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Nov 9 09:44:19 2017 +1000

    Merge pull request #1059 from tsimbalar/settings-abstract-param
    
    Add support for abstract class parameters in settings

[33mcommit 64e45ab19997ef0b543095835bcde613f5153f0f[m
Author: Thibaud DESODT <thibaud.desodt@dgtresor.gouv.fr>
Date:   Tue Nov 7 07:16:48 2017 +0100

    Make ReadFrom.KeyValuePairs not throw when duplicate keys
    
    Instead, take the last value of each key, to remove work from future Serilog.Settings.Combined

[33mcommit a80ea3e4d1cabdb178dca7ab09a005aaf3f8a09b[m
Author: Thibaud DESODT <thibaud.desodt@dgtresor.gouv.fr>
Date:   Tue Nov 7 07:02:03 2017 +0100

    Remove ReSharper false positive

[33mcommit 3e224a0d3d6529468e78e18147b9a236bea5b96c[m
Author: Thibaud DESODT <thibaud.desodt@dgtresor.gouv.fr>
Date:   Tue Oct 24 07:20:39 2017 +0200

    Remove "using System.Collections.ObjectModel"

[33mcommit f7323243d273b847e8d4a93bf83b808d0a9fc2d5[m
Author: Thibaud DESODT <thibaud.desodt@dgtresor.gouv.fr>
Date:   Tue Oct 24 07:07:14 2017 +0200

    Review : remove unnecessary method ExtractDirectives
    
    ... and useless test

[33mcommit 1c3449c19d7223cdfe085ebaacc99d299124d7da[m
Author: Thibaud DESODT <thibaud.desodt@dgtresor.gouv.fr>
Date:   Tue Oct 24 07:01:41 2017 +0200

    Review: No need to create a ReadOnlyDictionary after .ToDictionary()

[33mcommit 670a5ff179ddfbccb47952140a3e80a5ac0cb86c[m
Author: Thibaud DESODT <thibaud.desodt@dgtresor.gouv.fr>
Date:   Mon Oct 23 07:21:39 2017 +0200

    Pass a IReadOnlyDictionary to KeyValuePairSettings
    
    Refactored KeyValuePairSettings to accept a IReadOnlyDictionary<string, string> instead of IEnumerable<KeyValuePair<string, string>> in order to reveal the fact that key-value settings are not lazily consumed.
    
    Added tests on edge cases : duplicate keys, irrelevant settings.

[33mcommit f19f19685008251e808500919f53d9ed22eb96ec[m
Author: Thibaud DESODT <thibaud.desodt@dgtresor.gouv.fr>
Date:   Wed Nov 8 07:20:40 2017 +0100

    Add support for abstract class parameters in settings
    
    When a parameter is an abstract class, now accept the type name of a concrete subclass that has a default constructor ( the same way interfaces are supported)

[33mcommit 9f273756e8f97da3aaa7c51904e084e3550ad062[m
Merge: 3cd376c8 f03e544f
Author: Thibaud Desodt <tibo.desodt@gmail.com>
Date:   Tue Nov 7 11:02:46 2017 +0100

    Merge pull request #1055 from tsimbalar/resharper-warnings-3
    
    Remove more ReSharper warnings

[33mcommit f03e544fb5aefb901ab104f7f90b1c00ec55e6e3[m
Author: Thibaud DESODT <thibaud.desodt@dgtresor.gouv.fr>
Date:   Tue Nov 7 07:20:24 2017 +0100

    Get rid of ReSharper warnings "possible NullReferenceException" in tests
    
    On Type.GetMethod(), in net45 and net46 only

[33mcommit 3cd376c8c75ed49ab307c50cd80beaa06cbaecb7[m
Merge: c10e6253 1b4abc4e
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Oct 24 08:45:33 2017 +1000

    Merge pull request #1050 from tsimbalar/resharper-green-take2
    
    Fix last ReSharper warning

[33mcommit 1b4abc4e4bc56605b8147d5f3ba3f9d7eedf4fd3[m
Author: Thibaud DESODT <thibaud.desodt@dgtresor.gouv.fr>
Date:   Mon Oct 23 07:30:58 2017 +0200

    Fix last ReSharper warning
    
    Protect against possible null from base class

[33mcommit c10e625334f5b1599e1b593b045330770df1fb59[m
Merge: 8528ae34 f41eaf1b
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Oct 19 10:23:37 2017 +1000

    Merge pull request #1042 from tsimbalar/resharper-green-again
    
    Make Resharper green again

[33mcommit f41eaf1b07a6ebf77f1f60fb41a1d6cbc558bd56[m
Author: Thibaud Desodt <tibo.desodt@gmail.com>
Date:   Tue Oct 17 20:58:23 2017 +0200

    Added comments to ignore remaining warnings

[33mcommit 8528ae34434a51db2f3494643891ca6abcc87a4b[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Oct 17 09:39:18 2017 +1000

    Make the up-for-grabs link organization-wide [Skip CI]

[33mcommit abd9562d706a5b3ebfe56488aa006aa6c68ef47e[m
Author: Thibaud Desodt <tibo.desodt@gmail.com>
Date:   Mon Oct 16 07:54:27 2017 +0200

    Removed a few AssignNullToNotNullAttribute warnings in tests

[33mcommit 2f71498bc5a7778da4625ac3635ae423433d11d0[m
Author: Thibaud Desodt <tibo.desodt@gmail.com>
Date:   Mon Oct 16 07:51:21 2017 +0200

    Get rid of ThreadStaticFieldHasInitializer warnings

[33mcommit af0d217d9e4188a29122af6b4a3d8bc448c97768[m
Author: Thibaud Desodt <tibo.desodt@gmail.com>
Date:   Mon Oct 16 07:45:01 2017 +0200

    Get rid of HeuristicUnreachableCode warning

[33mcommit 4c231f2ab98e5b6923c1ea7347b92c2f1e2df43a[m
Author: Thibaud Desodt <tibo.desodt@gmail.com>
Date:   Mon Oct 16 07:44:16 2017 +0200

    Get rid of ParameterOnlyUsedForPreconditionCheck

[33mcommit ebf548c830cc1c5c2d75fa38d25e5367b048c014[m
Author: Thibaud Desodt <tibo.desodt@gmail.com>
Date:   Mon Oct 16 07:43:44 2017 +0200

    Get rid of 'hiding field' warning

[33mcommit 33d07a8282116faba336a89cdfc52a809437132d[m
Author: Thibaud Desodt <tibo.desodt@gmail.com>
Date:   Mon Oct 16 07:43:21 2017 +0200

    Get rid of "possible multiple enumerations" warning

[33mcommit 4fd5b26bae515a3456ce2f05ed169c31c79733c2[m
Author: Thibaud Desodt <tibo.desodt@gmail.com>
Date:   Mon Oct 16 07:38:20 2017 +0200

    ReSharper : changed "Redundant Argument Default value" inspection severity from Warning to Suggestion

[33mcommit 3377bf33e8fe30bcfbefd34862f664942ae187a9[m
Author: Thibaud Desodt <tibo.desodt@gmail.com>
Date:   Mon Oct 16 07:31:48 2017 +0200

    Remove unused declared variables

[33mcommit fc4b4f2244e8ac640a1fdd6cd7cfa1e5da184fd2[m
Author: Thibaud Desodt <tibo.desodt@gmail.com>
Date:   Mon Oct 16 07:30:00 2017 +0200

    Removed redundant qualifier

[33mcommit 3d8a2e7071fa68889ee6dc147fc945dd4262d8b7[m
Merge: b33a4aad 40731044
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Oct 11 10:07:40 2017 +1000

    Merge pull request #1034 from pardahlman/feature/for-context
    
    Add log level conditioned 'ForContext'

[33mcommit 40731044623120a5942e12929b0e3f77f4bc282f[m
Author: pardahlman <par.dahlman@fellowdeveloper.se>
Date:   Tue Oct 10 16:33:02 2017 +0200

    Adjust class names and method description
    
    In accordance with review input.

[33mcommit b33a4aad8b3da36a997b5b8bf16bd7ce090388d7[m
Merge: 5bcf1338 48b4757b
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Oct 10 07:29:58 2017 +1000

    Merge pull request #1033 from tsimbalar/967-no-sublogger-overrides
    
    Make it clear that Sub-logger MinimumLevel.Override is not supported

[33mcommit 48b4757b5a945da67b6309c024d07bcbf1461edf[m
Author: Thibaud Desodt <tibo.desodt@gmail.com>
Date:   Mon Oct 9 07:16:13 2017 +0200

    Notify that sub-logger MinimumLevel.Override is not supported
    
    Write a warning message to the SelfLog when using WriteTo.Logger() with a logger that has specified MinimumLevel overrides.
    Related to issue #967
    Related to discussion #1032

[33mcommit 23f7710ff746c8731b2b4fed021fcffe799db1b3[m
Author: pardahlman <par.dahlman@fellowdeveloper.se>
Date:   Sat Sep 30 07:55:08 2017 +0200

    Simplify ternary operation (not !)
    
    Relates to #1002

[33mcommit 6dc8d8e0857b484a118eb7ec3a55fec497f47da7[m
Author: pardahlman <par.dahlman@fellowdeveloper.se>
Date:   Sat Sep 30 07:54:02 2017 +0200

    Check if logger is null and throw
    
    Part of feedback for #1002

[33mcommit 2988d207e37d9a120fe917b1928daab16f628942[m
Author: pardahlman <par.dahlman@fellowdeveloper.se>
Date:   Fri Sep 29 20:27:43 2017 +0200

    Add log level conditioned 'ForContext'
    
    This method creates a logger with provided property
    if the log level is enabled

[33mcommit 5bcf1338c36880e2b3c6b5064aab75d55bee6ebd[m
Merge: 3bb85236 5569b1c3
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Sep 23 12:36:19 2017 +1000

    Merge pull request #1030 from tsimbalar/appsettings-timespan
    
    Add test to prove proper support for TimeSpan in AppSettings

[33mcommit 5569b1c3b8893e87d13bfff9ae355cb16b21d845[m
Author: Thibaud Desodt <tibo.desodt@gmail.com>
Date:   Fri Sep 22 17:13:13 2017 +0200

    Add test to prove proper support for TimeSpan in AppSettings

[33mcommit 3bb852361674b0d133f5a9e9c74d8c844de46867[m
Author: Thibaud Desodt <tibo.desodt@gmail.com>
Date:   Wed Sep 20 23:32:07 2017 +0200

    Support for LevelSwith in KeyValuePairSettings (#1020)
    
    * add support for declaring a named LoggingLevelSwitch
    
    key="level-switch:foo" value="Information" will create an instance of LoggingLevelSwitch named foo with initial level Information
    
    * add support for using a previously declared LoggingLevelSwitch to control MinimumLevel
    
    key="minimum-level:controlled-by" value="foo" will set MinimumLevel.ControlledBy with the previously declared named level-switch
    
    * [WIP] unit test for passing controlLevelSwitch to a sink
    
    * [Refactoring] extracted methods to make it clearer
    
    KeyValuePairSettings.ParseVariableDeclarationDirectives and KeyValuePairSettings.LookUpVariable
    
    * add the possibility to pass a declared LoggingLevelSwitch to a sink
    
    Passing a parameter directive the name of a declared Switch will pass it to the configuration method
    
    * add support for level-switch:Switch1 - value = "" and interpret it as level=information
    
    * Minor edits/typos in the unit tests
    
    * Change brittle tests in order not to rely on reflection to acces brittle private field
    
    * Simplify the code around "creating a new LoggingLevelSwitch"
    + throw a "nicer" error message when referencing an undeclared LoggingLevelSwitch
    
    * Add support for referencing a LoggingLevelSwitch from a minimul level override
    
    key="serilog:minimum-level:override:System" value ="switchName" will bind the override to a switch that may be controlled through the sink
    
    * Enforce declaration of switch as level-switch:$switchName
    ... and use $ also when referencing the declared swith
    This helps disambiguate between a variable and the minimum level for a switch ... and makes sure noone is goind to declared a switch with name "Information"
    
    * Minor tweaks after review
    
    * Explicitly throw a helpful FormatException when specifiying a level-switch with a switch name not starting with a $ sign
    
    * Minor refactoring of ParseNamedLevelSwitchDeclarationDirectives
    use Dictionary.Add() instead of Dictionary[] to make it mor explicit that uniqueness is implied
    
    * Refactoring : use TryGetValue instead of ContainsKey
    
    * Refactoring : renamed LookUpSwitchByNameOrThrow to LookUpSwitchByName for consistency

[33mcommit 81a902307468ff6143c30225610f2fa3f6d9399e[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Sep 21 07:31:45 2017 +1000

    Minor version bump, new features incoming [Skip CI]

[33mcommit 7c8aa4f45c228403b5b6a083fcde49fd548ea901[m
Merge: 47e72dcd e6813730
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Sep 20 13:37:12 2017 +1000

    Merge pull request #1018 from serilog/include-source
    
    Include source in package builds

[33mcommit 47e72dcd610432e37e67fc04e6918395cb465389[m
Merge: 8afea4b1 0b7da58b
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Sep 17 13:23:07 2017 +1000

    Merge pull request #1025 from tsimbalar/child-logger-tests
    
    Added more (passing) tests about Child Loggers and overrides

[33mcommit 8afea4b1a5103d581570d9ec52db23ab5c9dc5a8[m
Merge: 2874c855 1f87569f
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Sep 17 13:20:04 2017 +1000

    Merge pull request #1026 from tsimbalar/contributing-fix
    
    Meta PR to fix PR Template [Skip CI]

[33mcommit 1f87569f234504b11f0ad8a3910dcaa9ee0cdab4[m
Author: Thibaud Desodt <tibo.desodt@gmail.com>
Date:   Sat Sep 16 13:48:57 2017 +0200

    Fixed dead link to CONTRIBUTING.md
    
    The link in PULL_REQUEST_TEMPLATE.md pointed to a 404 (https://github.com/serilog/serilog/CONTRIBUTING.md)

[33mcommit 0b7da58b172f4258a7e173adc1189a20a66db81d[m
Author: Thibaud Desodt <tibo.desodt@gmail.com>
Date:   Fri Sep 15 16:58:55 2017 +0200

    Some more passing tests around child logger overrides

[33mcommit 4d9a0f90b976d7ef3aa65170c635a28363c3be66[m
Author: Thibaud Desodt <tibo.desodt@gmail.com>
Date:   Fri Sep 15 07:21:41 2017 +0200

    Added more tests for simple overrides w/ child loggers

[33mcommit f292b59e48bc652420ed2cb627d15d5597d66a24[m
Author: Thibaud Desodt <tibo.desodt@gmail.com>
Date:   Thu Sep 14 22:28:08 2017 +0200

    Adding more tests to cover child loggers

[33mcommit 2874c8556b8202d3518d704651120080bf7a5c51[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Thu Sep 14 07:12:58 2017 +1000

    Grammar fix [Skip CI]

[33mcommit 0b2e4edc39391e5ae3475a916e50d55934bf0465[m
Merge: 67ff3097 279d51c8
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Sep 5 08:34:45 2017 +1000

    Merge pull request #1019 from merbla/code-of-conduct-and-contrib
    
    Code of Conduct, Contributing Guide & templates for issues and PRs [Skip CI]

[33mcommit 279d51c833ddcd495ac09260c1e67e866a36486c[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Mon Sep 4 21:05:00 2017 +1000

    #842 Added issue and PR templates. Also added contrib guidelines.

[33mcommit 5bdde89bb4df7e75ab52054906b61a24da320089[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Mon Sep 4 21:03:53 2017 +1000

    Removed detailed code in favour of Contributor Covenant link.

[33mcommit d95ffa4f68dc0365605bfe8c5e25fa4024399646[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Thu Aug 31 20:36:30 2017 +1000

    Added Code of Conduct
    
    * Reference - https://www.contributor-covenant.org/:

[33mcommit e68137306fdd029e879ed3c0cd3b909368a6a6b8[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Aug 30 16:32:25 2017 +1000

    Preserve informational version when building on master

[33mcommit 9a1b806c5c95d5a15797a55392f42d103ee6f8f4[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Aug 30 10:33:22 2017 +1000

    Include source in package builds; use implicit build via dotnet pack

[33mcommit 67ff3097304634c405b4a9b0e5f282a6949f150d[m
Merge: f989fd66 118f8698
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Jul 29 07:37:18 2017 +1000

    Merge pull request #1007 from mariozski/dev
    
    Implementation of #1006

[33mcommit 118f86988e642ded3ed927a21e8b987172e49ae9[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Jul 29 07:31:19 2017 +1000

    Minor documentation wording nitpicks

[33mcommit ef2a5dea7cae9a1fa766ce4aabc83ea30d9c854b[m
Author: Mariusz Zielinski <mariusz@zielinski.me>
Date:   Fri Jul 28 22:46:08 2017 +0200

    Reset method refactoring

[33mcommit d8b6e94734216a42fb5214fbf7befe19b592e9e4[m
Author: Mariusz Zielinski <mariusz@zielinski.me>
Date:   Fri Jul 28 01:25:02 2017 +0200

    Added Suspend and Reset methods to LogContext to make possible clearing enrichers especially in async context.

[33mcommit f989fd66f752c7192cf9bce6a8f23e29b9dcb5f5[m
Merge: c55e14b9 d490f784
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Jul 5 07:54:22 2017 +1000

    Merge pull request #996 from skomis-mm/nativefix
    
    Enable .NET Native on AppVeyor + workaround on compiler error for #993

[33mcommit d490f784092adfc689d60e4851babe9003b4bdf1[m
Author: Sergey Komisarchik <komisarchik@media-saturn.com>
Date:   Tue Jul 4 09:13:06 2017 +0300

    add missed Serilog.UwpTests_TemporaryKey.pfx

[33mcommit f1424a94ef6a4f5adef980c4d78884fd317dbd18[m
Author: Sergey Komisarchik <komisarchik@media-saturn.com>
Date:   Tue Jul 4 08:49:12 2017 +0300

    rename: test/Serilog.UWP -> test/Serilog.UwpTests

[33mcommit 7a70a021bcc9d30c97f376029ce414d9b5c3912a[m
Author: Sergey Komisarchik <komisarchik@media-saturn.com>
Date:   Mon Jul 3 22:14:19 2017 +0300

    .net native compiler error workaround fix

[33mcommit 19f49a74df51708ff9f816506e4633bc4145df72[m
Author: Sergey Komisarchik <komisarchik@media-saturn.com>
Date:   Mon Jul 3 21:59:51 2017 +0300

    BuildNativeUWP.ps1 fix
    - exit with non zero-code on error

[33mcommit 813828dc9a2e9e3bec9069594dc6dc65fdfdc596[m
Author: Sergey Komisarchik <komisarchik@media-saturn.com>
Date:   Mon Jul 3 14:15:05 2017 +0300

    enable .net native toolchain for ci

[33mcommit c55e14b9e5f82191c68a45b7570a4d6da42aaf6c[m
Merge: 948ab569 2f4949fe
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Jul 3 08:30:48 2017 +1000

    Merge pull request #992 from skomis-mm/25
    
    LogContext's remoting issue fix for .NET 4.5

[33mcommit 948ab5692e99d04ce63d72ecb0630022261fd4ac[m
Merge: 726576b2 26b8f82a
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Jul 3 08:10:49 2017 +1000

    Merge pull request #994 from blachniet/patch-2
    
    Remove Rager releases badge

[33mcommit 26b8f82ad969218f2068b6fdff8ebdef6c97d8ff[m
Author: Brian Lachniet <blachniet@gmail.com>
Date:   Sun Jul 2 09:31:04 2017 -0400

    Remove Rager releases badge

[33mcommit 2f4949fe9ec1948a0943cb7881c69a2eae1f3dfc[m
Author: Sergey Komisarchik <komisarchik@media-saturn.com>
Date:   Sat Jul 1 16:52:30 2017 +0300

    LogContext's remoting issue fix for .NET 4.5

[33mcommit 726576b299141a627339d8c6f35b19aadab0e935[m
Merge: e3153e5d 1255f839
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Jun 22 08:20:47 2017 +1000

    Merge pull request #988 from merbla/travis-trusty-group
    
    Travis CI Image - use edge group

[33mcommit 1255f839c16500e94f0febbedd7083fd91031731[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Wed Jun 21 17:21:14 2017 +1000

    Update to use edge group.
    
    * As per https://blog.travis-ci.com/2017-06-19-trusty-updates-2017-Q2

[33mcommit e3153e5d816a8e31b27a11c12bc14962c52d2323[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Wed Jun 21 14:27:26 2017 +1000

    Updated Changes with 2.5.0 release  [Skip CI]

[33mcommit feb4d9a1c0a73f976a6db4e47416fa0f10dc8f06[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Jun 21 10:25:11 2017 +1000

    Dev version bump [Skip CI]

[33mcommit 62f26ffcd2200297df64e8715b9492317f87a778[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Jun 15 08:47:27 2017 +0200

    Fix defined constants for net46 target

[33mcommit e42245c0db0931e1fffdd72a0729361c60e877bc[m
Merge: e9534012 e205c078
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Jun 13 09:50:01 2017 +0200

    Merge pull request #977 from nblumhardt/output-improvements
    
    Output and rendering improvements

[33mcommit e205c078b9fd0704e7bd778e2a214049472535df[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Jun 7 20:10:35 2017 +1000

    Fewer arg checks; seal message template token classes; encourage inlining

[33mcommit ce2caeffd0f8fdb87676bf32ccd36a66654e85e9[m
Merge: c17ea150 e9534012
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Jun 7 08:34:08 2017 +1000

    Merge branch 'dev' into output-improvements; enable `MemoryDiagnoser` on select benchmarks.

[33mcommit e9534012ce0552f869c5d3b4318f6b1414441be3[m
Merge: cfb1ef9e e23e2bb7
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Jun 7 08:26:59 2017 +1000

    Merge pull request #981 from skomis-mm/less_alloc
    
    PropertyValueConverter improvements

[33mcommit e23e2bb7249631ee27580b3bd3cc00c5c9029141[m
Author: Sergey Komisarchik <komisarchik@media-saturn.com>
Date:   Thu Apr 6 23:12:45 2017 +0300

    less allocations

[33mcommit c17ea150ec9587dd9ab78b65845502d603b30dbf[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Jun 6 07:46:56 2017 +1000

    Prevent xunit from shadow-copying test assemblies

[33mcommit 4b0e2bef32c482f3d2ee5fc4f36ac707298772e4[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Jun 5 08:50:45 2017 +1000

    Fix some dodgy string formatting benchmarks

[33mcommit 9b99d7e29d97c52e44ca79b4e8df051df90dacea[m
Merge: 3cec4bdd 0308bc70
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Jun 5 08:38:25 2017 +1000

    Merge branch 'bdn-update' into output-improvements

[33mcommit 0308bc702686d886b4a00738dc6acacc0cf86064[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Jun 5 08:37:46 2017 +1000

    Update BenchmarkDotNet to 0.10.6

[33mcommit 3cec4bdd57dc58da52eb1e2955c6d7fe310724d1[m
Merge: 5f5770d2 cfb1ef9e
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Jun 5 07:13:58 2017 +1000

    Merge branch 'dev' of https://github.com/serilog/serilog into output-improvements

[33mcommit cfb1ef9e8ef0e6b48a3b0f0e77d5bcb4ca94b4da[m
Merge: 4a97ef0b 0596b33e
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Jun 5 07:10:14 2017 +1000

    Merge pull request #978 from merbla/perf-build
    
    Independent Build for Perf Tests.

[33mcommit 4a97ef0b8acd8fe10b86474b17f3b861aa604d89[m
Merge: 8b68856e 63942761
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Jun 5 07:08:35 2017 +1000

    Merge pull request #979 from merbla/travis-build-remove-install
    
    Tooling Cleanup - Rely on Travis.yml for Build requirements

[33mcommit 639427613a990666115644b801ac6602e9cf5516[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Sat Jun 3 14:52:47 2017 +1000

    Removed install from build script and rely on Travis yml

[33mcommit 8b68856ef81558a696cc32e87581dedb8c4be4fb[m
Merge: 9376b930 8c33f235
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Jun 3 11:21:18 2017 +1000

    Merge pull request #976 from nblumhardt/value-tuple
    
    ValueTuple to sequence value conversion

[33mcommit 8c33f2357fcffb3f89fbac19b057316a56bd6754[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Jun 3 11:13:40 2017 +1000

    Review feedback/test case names.

[33mcommit 0596b33eea8fa00ab8dafcd03c08952c7b29dfc7[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Sat Jun 3 08:59:18 2017 +1000

    Independent Build for Perf Tests.

[33mcommit 5f5770d298c6fc4872b2d2eed0250eda8c342b41[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Jun 2 08:43:48 2017 +1000

    Render text token through the external method

[33mcommit 8a57c075382d2cc67c5279f3a938de2f011afc91[m
Merge: 3ba9d2a1 9376b930
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Jun 1 21:21:24 2017 +1000

    Merge branch 'dev' into output-improvements

[33mcommit 3ba9d2a1669133b7535fa34b90282b3408bb5d51[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Jun 1 21:17:02 2017 +1000

    Tests for {Message:l} and {Message:j} formatting

[33mcommit 2ce90e416bb3483e8d64e8b9bf5e844d7df921e1[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Jun 1 20:39:30 2017 +1000

    Improve flow of MessageTemplateRenderer

[33mcommit a74938cda85d00257e28e661649a277a0e21aacc[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Jun 1 20:14:03 2017 +1000

    Namespace layout and RESHARPER GREEN!

[33mcommit 888a49866d65256bb75ed1f59c019f725c1cbac8[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Jun 1 19:47:15 2017 +1000

    Review feedback - avoid package dependency on platforms without native ValueTuple

[33mcommit 9376b930db9e1da8f9922baf33b52c0d60af3071[m
Merge: 887a2ed0 b4888ceb
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Jun 1 13:52:11 2017 +1000

    Merge pull request #960 from nblumhardt/efficient-output-properties
    
    Output template formatting harness

[33mcommit ffee0dba4d4182f913cc71e2b4284a7f72b33832[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed May 31 22:22:45 2017 +1000

    Separate rendering of message templates from their representation

[33mcommit b02acd281df277d8d0d6282e6377344f309a4afc[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed May 31 15:12:18 2017 +1000

    Separate current and obsolete formatting code

[33mcommit 391882a3dd3e1c86f0ea116b7ad71f3f9ed3b761[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed May 31 14:19:57 2017 +1000

    Fix tests

[33mcommit d2dd76d2f62a8c4eb35c333077bf25e00b625d40[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed May 31 14:09:54 2017 +1000

    Initial work for #958 - stop allocating output properties during rendering

[33mcommit b4888cebe7fb27fcf2b78b2965c9785bbd8d11ec[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed May 31 12:12:53 2017 +1000

    Added message template rendering benchmark

[33mcommit f3d2edd4a3efcd8cf2e6c1559e0ee2a18d912550[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed May 31 10:37:53 2017 +1000

    Minimal test cases

[33mcommit 3d80e9ee01e8f952f45daff378549b518f650940[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed May 31 10:29:36 2017 +1000

    Remove redundant blocks

[33mcommit ce9b893c76785929d44632c6fb03c9c6d8321a85[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed May 31 10:26:29 2017 +1000

    A quick extract-method refactoring

[33mcommit e3910e9f28f9302a6eb8f759a4fe365a5ed714e0[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed May 31 09:46:51 2017 +1000

    ValueTuple to sequence value conversion

[33mcommit 855593438417fd91870a296be8d1adcb17e65150[m
Merge: f299b58e 916b9f46
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon May 29 11:27:58 2017 +1000

    Merge branch 'dev' into efficient-output-properties

[33mcommit 887a2ed07761137557aba5969856f85f6a4e9953[m
Merge: b01f1574 60b71b4e
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon May 29 07:35:39 2017 +1000

    Merge pull request #974 from nblumhardt/logcontext-push
    
    LogContext Push() and Clone()

[33mcommit 60b71b4ee6bae6d8607e0fafa8a70096b92da69f[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon May 29 07:22:45 2017 +1000

    Review feedback items

[33mcommit b01f1574656f17e6ed00db14bf7383b11f19eeb8[m
Merge: 916b9f46 c12f5742
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon May 29 07:14:16 2017 +1000

    Merge pull request #972 from merbla/appveyor-2017
    
    VS2017 Appveyor image and 1.0.4 SDK for Travis

[33mcommit 6190ca819f3f3a63faa282170f48e23ebf82d37c[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun May 28 08:36:01 2017 +1000

    #773 - clone and re-import LogContext.

[33mcommit 36f8429022beb651205cd93356bb4e7e451fb9d4[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun May 28 08:29:40 2017 +1000

    Rename `LogContext.PushProperties()` to `LogContext.Push()`; add a non-params overload to reduce allocs. Partially covers 773.

[33mcommit c12f5742d6a6e3338ed322f79fffb12be02f7acf[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Sun May 21 08:37:09 2017 +1000

    VS2017 Appveyor image and 1.0.4 SDK for Travis

[33mcommit 916b9f461e28fd5fd864e5ea557e9523a3af5c93[m
Merge: 643dfa4f 5944e29b
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Sat Apr 8 09:06:53 2017 +1000

    Merge pull request #961 from nblumhardt/parser-exception
    
    Parsing longer property tokens with zero-length names

[33mcommit 5944e29b1bb04aa9fcb6376112cd2089d2a494fe[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Apr 8 08:15:29 2017 +1000

    Fixes #959 - exception when parsing longer property tokens with zero-length names

[33mcommit f299b58e20c9b84e77ff050146e0dcb23be09218[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Apr 8 08:01:11 2017 +1000

    Output template formatting harness

[33mcommit 643dfa4f380ea6babf63586cd9737bcac85bdac4[m
Author: Yury Pliner <yury.pliner@gmail.com>
Date:   Thu Apr 6 03:17:46 2017 +0500

    Support {Properties} in output templates (#944)
    
    Fixes #825 - output template support for `Properties`

[33mcommit 32e748e78e0e203b36d856ed23b2df2a96a53bc9[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Apr 2 18:27:08 2017 +1000

    Bump minor version - `LoggerSinkConfiguration.Wrap()` added API

[33mcommit f566978c56047b1488265b138f53d8549d70a9b3[m
Author: John Du Hart <john@johnduhart.me>
Date:   Sun Apr 2 04:26:16 2017 -0400

    Sink wrapping (#955)
    
    `LoggerSinkConfiguration.Wrap()`: Improve configuration story for wrapper sinks like Serilog.Sinks.Async

[33mcommit d073dcf896f89d76805ae7fc9a97e44830d5672e[m
Merge: a6b3cd0c 0d7ad428
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Mar 22 17:32:21 2017 +1000

    Merge pull request #951 from merbla/release-2.4-changes
    
    Missing list for 2.4 for changes.md

[33mcommit 0d7ad428fc8011803dab2ab283b93843186d9933[m
Author: Matthew Erbs <merbs@splunk.com>
Date:   Wed Mar 22 14:37:31 2017 +1000

    Missing list for 2.4 for changes.md

[33mcommit a6b3cd0cffb768b9adba29c91d3a4bf0e8dc70c4[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Mar 19 11:50:57 2017 +1000

    Fix the H1 markdown [Skip CI]

[33mcommit b37f1583217f2e21601ba480ea294ffbaad4c856[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Mar 19 11:49:54 2017 +1000

    It's 2017 [Skip CI]

[33mcommit 7d47e63ee8595a3ba0df444cffe231b38657f6b5[m
Merge: 4c9c3c80 5a9114f8
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Mar 10 17:47:04 2017 +1000

    Merge pull request #947 from nblumhardt/formatter-constant-newline
    
    Cache the {NewLine} literal property to avoid an allocation on each render

[33mcommit 5a9114f8b5e0a5d6d4466fe5b8f616de226fa428[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Mar 10 15:06:58 2017 +1000

    Cache the {NewLine} literal property to avoid an allocation on each render

[33mcommit 4c9c3c804c190f9dcfc9ae859f09a71f585b7c4c[m
Merge: 9b581e31 568cfa9b
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Mar 10 14:14:01 2017 +1000

    Merge pull request #946 from adamchester/dotnet-1.0.1
    
    Use dotnet core sdk 1.0.1

[33mcommit 568cfa9b7ef94e9a2c6b1dadc6faa89e53391136[m
Author: Adam Chester <adamchester@gmail.com>
Date:   Fri Mar 10 06:40:24 2017 +1000

    Use dotnet core sdk 1.0.1

[33mcommit 9b581e31eb6d0f7f480995f9d828998b591a1d7a[m
Merge: 7112d6b0 3b9f10ca
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Mar 9 12:46:34 2017 +1000

    Merge pull request #939 from adamchester/rc4-tooling
    
    VS 2017 / dotnet 1.0.0 tooling

[33mcommit 3b9f10ca05a559518972e67583eda98d33aa55c1[m
Author: Adam Chester <adamchester@gmail.com>
Date:   Thu Mar 9 10:18:13 2017 +1000

    Disable travis OSX builds

[33mcommit 9ae6dfcf5b4fd8fcb63e88554c36f93b5657233c[m
Author: Adam Chester <adamchester@gmail.com>
Date:   Thu Mar 9 06:03:13 2017 +1000

    Use magic sln project guids,
    
    so that VS features like live csproj file editing work correctly.

[33mcommit 3a46862a8cdc03176db95fb0b0257205bf41ddb4[m
Author: Adam Chester <adamchester@gmail.com>
Date:   Wed Mar 8 16:27:31 2017 +1000

    Fix `run_perf_tests.sh`

[33mcommit e67472698d97ac703739b1780070cf08e66ef5cf[m
Author: Adam Chester <adamchester@gmail.com>
Date:   Wed Mar 8 11:35:30 2017 +1000

    DisableImplicitFrameworkReferences

[33mcommit 64d1fe01eb93a1f920d2a62cf688dd9b04f06641[m
Author: Adam Chester <adamchester@gmail.com>
Date:   Wed Mar 8 11:10:35 2017 +1000

    Update CLI to 1.0.0

[33mcommit 5ee4695d11d12338c0f0b936d16d24194c5761b3[m
Author: Adam Chester <adamchester@gmail.com>
Date:   Sun Mar 5 12:07:14 2017 +1000

    Increase ulimit

[33mcommit a825eff70dd9654d2ded6045567a4f5ddd176ac7[m
Author: Adam Chester <adamchester@gmail.com>
Date:   Sun Mar 5 11:52:32 2017 +1000

    Only build perf tests on OSX/Linux (not run)

[33mcommit 7b2c70f7383c2768a6f1242b4952627852d59b61[m
Author: Adam Chester <adamchester@gmail.com>
Date:   Sun Mar 5 11:43:25 2017 +1000

    Prevent appveyor from auto-running tests

[33mcommit 33ae3ec1b98f2c221932e643fe411f88b31a817f[m
Author: Adam Chester <adamchester@gmail.com>
Date:   Sun Mar 5 11:29:15 2017 +1000

    Perf tests: BDN 0.10.3 + netcoreapp1.1

[33mcommit 76f599e2fc08926f1bc948e8d8ca9381888613cc[m
Author: Adam Chester <adamchester@gmail.com>
Date:   Sun Mar 5 11:22:17 2017 +1000

    Produce symbol packages again

[33mcommit b2b95aa4c97f09e73d2309fe5420e151ef8a6eab[m
Author: Adam Chester <adamchester@gmail.com>
Date:   Sun Mar 5 08:30:11 2017 +1000

    Improve travis(osx/linux) build

[33mcommit 1dfb049b23517aa69ebddb92e64a64161324ace9[m
Author: Adam Chester <adamchester@gmail.com>
Date:   Sun Mar 5 08:18:05 2017 +1000

    Stop using beta test libraries

[33mcommit df69837d6ce6e2c09597b58726194e5d7f294c19[m
Author: Adam Chester <adamchester@gmail.com>
Date:   Sun Mar 5 08:11:55 2017 +1000

    Fix travis build, remove OpenCover

[33mcommit 273c191623015e2a5b31005abe3e03bce22ee58b[m
Author: Adam Chester <adamchester@gmail.com>
Date:   Tue Feb 14 20:56:56 2017 +1000

    Adjust codcov test command

[33mcommit 6087532764e364516226b63fc6ee510f5d3dc222[m
Author: Adam Chester <adamchester@gmail.com>
Date:   Tue Feb 14 20:38:53 2017 +1000

    travis: use `osx_image` = `xcode7.3`,
    
    because `xcode7.2` is deprecated

[33mcommit ee98b3e77ac6202fa90c59a501fa42aab34dd897[m
Author: Adam Chester <adamchester@gmail.com>
Date:   Tue Feb 14 20:38:20 2017 +1000

    Fix `netstandard1.3` defines and references

[33mcommit 079a2265d08f465a168672df19998cc8a691588d[m
Author: Adam Chester <adamchester@gmail.com>
Date:   Sat Feb 11 12:23:44 2017 +1000

    Remove `rc4` from the dotnet cli install script path

[33mcommit b0b5eee5c367867ddab20391a3c9ee149afeec62[m
Author: Adam Chester <adamchester@gmail.com>
Date:   Sat Feb 11 12:18:19 2017 +1000

    Bring back `TestMinimumLevelOverrides`

[33mcommit e89d7cb81210bc91036ce9b832aab56501922841[m
Author: Adam Chester <adamchester@gmail.com>
Date:   Sat Feb 11 12:17:48 2017 +1000

    Tooling RC4-004771

[33mcommit e5b1f51ff075e4591ffec0a287ccad9cc24ca85b[m
Author: Adam Chester <adamchester@gmail.com>
Date:   Sat Feb 4 08:29:59 2017 +1000

    Tooling RC3

[33mcommit 7112d6b032e6bdd5b7594465926cc5de48b41b16[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Feb 3 11:15:55 2017 +1000

    Dev version bump [Skip CI]

[33mcommit 05b472b794c7aecc2d539466dfa8fca1f2cbdb23[m
Merge: 6118a6f5 c5747760
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Jan 30 07:42:23 2017 +1000

    Merge pull request #932 from merbla/redo-osx-build-for-travis
    
    Enable builds for OSX on Travis #824

[33mcommit c57477606c0c0292f51af20fb2783a4392d94b60[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Sat Jan 28 13:49:04 2017 +1000

    Changed to use Travis OSX example from dotnet/docs.

[33mcommit 6118a6f5958fc42b4d74e80236bc7151983ffdbf[m
Merge: 79cb2b18 c0651996
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Jan 19 15:07:02 2017 +1000

    Merge pull request #925 from nblumhardt/filter-config
    
    Refactor key-value settings to consistently apply configuration methods

[33mcommit c06519962178e6ee953d003fb10e9dc10e235f35[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Jan 4 16:16:47 2017 +1000

    Refactor key-value settings to consistently apply audit-to, write-to, filter and enrich configuration methods

[33mcommit 79cb2b1877114f670816a1879f0b3fe0f9a25460[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Dec 20 14:42:41 2016 +1000

    Stack Overflow is two words [Skip CI]

[33mcommit 88822ec77e82eac21b18e9412c4db5340fdb50fa[m
Merge: 8e3460ff 1e2ecf1a
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Dec 1 07:37:16 2016 +1000

    Merge pull request #909 from clarkis117/dev
    
    Added tests for the rest of ILogger and added support for SilentLogger to Method Overload tests

[33mcommit 8e3460ff187f26d17e192f7cdb77d48ecc925c08[m
Merge: bfcae1b2 06f2f95a
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Nov 29 15:01:20 2016 +1000

    Merge pull request #913 from skomis-mm/issue784
    
    Include AssemblyInformationalVersion attribute to the with commit hash

[33mcommit 06f2f95ae756219a998649d4ad4194ac5f5bd319[m
Author: Sergey Komisarchik <komisarchik@media-saturn.com>
Date:   Tue Nov 29 07:49:08 2016 +0300

    log $buildSuffix

[33mcommit 8070720866a87d1f3b4b89fa73fa27468af2cbde[m
Author: Sergey Komisarchik <komisarchik@media-saturn.com>
Date:   Tue Nov 29 03:50:45 2016 +0300

    Include AssemblyInformationalVersion attribute to the with commit hash

[33mcommit 1e2ecf1adbc1eb698087ad2896547c2676288ca1[m
Author: Joshua Clark <clarkis117@live.com>
Date:   Sat Nov 26 21:17:07 2016 -0500

    addressing some partials and misses along with some code clean up

[33mcommit 108d21c6f604101795fe3e9a43be6bf788262273[m
Author: Joshua Clark <clarkis117@live.com>
Date:   Fri Nov 25 00:30:17 2016 -0500

    Added test for IsEnabled methods, added additional method invokes to eliminate some partials and misses

[33mcommit eb2b2b27aad65415e3fffb616cf7eafa0a8c1651[m
Author: Joshua Clark <clarkis117@live.com>
Date:   Wed Nov 23 23:00:47 2016 -0500

    added message template attribute check

[33mcommit ada1df031be1402f394304e83580059da6405c20[m
Author: Joshua Clark <clarkis117@live.com>
Date:   Wed Nov 23 22:53:39 2016 -0500

    Added tests for the rest of ILogger and add support for SilentLogger

[33mcommit bfcae1b2ceef42bc50b6e75a672429beb36a4007[m
Merge: 4dd714bc d79f0392
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Nov 24 09:45:16 2016 +1000

    Merge pull request #907 from skomis-mm/dev
    
    Dispose audit sinks on ILogger.Dispose()

[33mcommit d79f0392c3c2291b0062be8e0cbc674fa2f7472d[m
Author: Sergey Komisarchik <komisarchik@media-saturn.com>
Date:   Thu Nov 24 02:34:50 2016 +0300

    cosmetics on disposing audit sinks

[33mcommit 6099d92ccadd8db4a77be2cf7635d9663d7ef82b[m
Author: Sergey Komisarchik <komisarchik@media-saturn.com>
Date:   Thu Nov 24 02:12:07 2016 +0300

    dispose audit sinks

[33mcommit 4dd714bc456ce2a9127a46751dba4878a3bdb521[m
Merge: 8d3c9c51 b07de29a
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Nov 23 16:47:39 2016 +1000

    Merge pull request #903 from clarkis117/dev
    
    ILogger API conformance fixes in Serilog.Log

[33mcommit 8d3c9c5141dde285ff3bd92bec34fe8d9a92b99c[m
Merge: f3c0925a 5a19e4ae
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Nov 23 16:42:13 2016 +1000

    Merge pull request #905 from nblumhardt/f-serialtests
    
    Disable parallel test execution

[33mcommit 5a19e4ae92fefafea215db39735a3ba2412791ef[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Nov 23 12:30:33 2016 +1000

    Add the all-important missing file

[33mcommit 844369f97cce55882bad75b3d85b3222e01130ef[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Nov 23 12:23:01 2016 +1000

    Disable parallel test execution

[33mcommit b07de29a1ab6ad58620a856faec6b35fe79b2265[m
Author: Joshua Clark <clarkis117@live.com>
Date:   Fri Nov 18 13:02:07 2016 -0500

    revert changes to ForContext(ILogEventEnricher[] enrichers)

[33mcommit 89a47d30be6c0b3839aba263b99d5722ef61997d[m
Author: Joshua Clark <clarkis117@live.com>
Date:   Fri Nov 18 00:18:53 2016 -0500

    ILogger API conformance fixes in Serilog.Log

[33mcommit f3c0925a614d25b3850cd4bcf2b75e33fc3b1f14[m
Merge: 6e603056 3e94c137
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Nov 18 09:54:08 2016 +1000

    Merge pull request #902 from clarkis117/dev
    
    Moved convention tests, and added support for other ILogger based classes

[33mcommit 3e94c1373a1699d4d451da1308033cadd2e4c125[m
Author: Joshua Clark <clarkis117@live.com>
Date:   Thu Nov 17 18:47:31 2016 -0500

    Add tests that touch to static Serilog.Log to their own test collection

[33mcommit 80cf2e6ec96afabfd13b25c3b09806fdaa891936[m
Author: Joshua Clark <clarkis117@live.com>
Date:   Tue Nov 15 17:34:25 2016 -0500

    Moved convention tests to separate file, and added tests for static Log and ILogger

[33mcommit 6e603056fe0c9397218a0073e508c387abb2bf89[m
Merge: 9dd1be8d 42364e94
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Nov 11 07:46:08 2016 +1000

    Merge pull request #877 from krajek/payload-limiting
    
    Payload limiting - collection limit

[33mcommit 42364e9471abdac1bfd403b2f6218869f1a8633d[m
Author: Artur Krajewski <krajek.dev@gmail.com>
Date:   Thu Nov 10 09:44:05 2016 +0100

    Fixed nameof arg in PropertyValueConverter range checking.

[33mcommit 7f3c06e7b3c8508c676d5cda2df1195b047249d8[m
Author: Artur Krajewski <krajek.dev@gmail.com>
Date:   Thu Nov 10 09:40:38 2016 +0100

    Fixed null-checking in destructuring conf constructor.

[33mcommit 967339af57693cd7b0fe0f5bd3438b8ca37d35ea[m
Author: Artur Krajewski <krajek.dev@gmail.com>
Date:   Thu Nov 10 09:39:00 2016 +0100

    Fixed typo from last commit(accidentally changed string related name)

[33mcommit 6264881ad382f35a2012746f5d21d7904fae0220[m
Author: Artur Krajewski <krajek.dev@gmail.com>
Date:   Wed Nov 9 20:25:48 2016 +0100

    'MaximumCollectionLength' -> 'MaximumCollectionCount'
    
    Length is kind-of specific to arrays, count is more general, suitable for all IEnumerable.

[33mcommit 9dd1be8dec9de97cfe520d1254a83ecfbb02a4ed[m
Merge: f803d8f6 6b3425a4
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Nov 8 14:33:20 2016 +1000

    Merge pull request #896 from clarkis117/dev
    
    Convention tests for log level write methods in Serilog.Core.Logger

[33mcommit 6b3425a4b811db248b0984f35353e2618a7dbc8d[m
Author: Joshua Clark <clarkis117@live.com>
Date:   Sat Nov 5 05:04:24 2016 -0400

    addressing feedback: changed logger configuration, condensed tests into theory, fixed some comments, add way to bubble up exception messages

[33mcommit 1a484f207e7c5fa7d7fe584230e03a91eb149f92[m
Author: Joshua Clark <clarkis117@live.com>
Date:   Fri Nov 4 08:16:10 2016 -0400

    convention tests for log level write methods

[33mcommit f803d8f6b7777f471b6a45da5db0fb5d22639370[m
Merge: 17c40e82 150f0f9a
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Nov 2 13:02:28 2016 +1000

    Merge pull request #887 from clarkis117/dev
    
    fix for Issue #885, formatting floating point special values NaN/PositiveInfinity/NegativeInfinity as JSON strings and unit tests

[33mcommit 150f0f9a0e8e5514db81e6e24025e58c21b6256d[m
Author: Joshua Clark <clarkis117@live.com>
Date:   Tue Nov 1 22:57:57 2016 -0400

    Requested changes: invariant culture and literal expected values in tests

[33mcommit 8a017eef521406e408d6112d63f343797c192233[m
Author: Joshua Clark <clarkis117@live.com>
Date:   Tue Nov 1 08:48:37 2016 -0400

    replaced switched on type with separate functions

[33mcommit 17c40e82b552948c46639e609a383fe083f62719[m
Merge: ba795acf 675889b1
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Oct 31 09:50:43 2016 +1000

    Merge pull request #888 from optical/minimum-level-override-config
    
    Allow overriding log level via config files

[33mcommit 675889b156d634287097fd2bdd8b030149f8f524[m
Author: Jared Klopper <spuzzy2@hotmail.com>
Date:   Sun Oct 30 21:11:54 2016 +1300

    Allow overriding log level via config files

[33mcommit 3e47423e563cca24dd917191d6405acbbcb339fb[m
Author: Joshua Clark <clarkis117@live.com>
Date:   Fri Oct 28 19:20:50 2016 -0400

    Fix for issue #885, formatting floating point special values NaN/PositiveInfinity/NegativeInfinity as JSON strings and unit tests

[33mcommit ba795acfb221bd8f90c1997ac4c8cc16fa8e7067[m
Merge: 0c8f6461 97f297c2
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Oct 20 16:13:02 2016 +1000

    Merge pull request #883 from skomis-mm/msgtplcache
    
    Improve MessageTemplateCache performance

[33mcommit 97f297c288557034ff99ff13eabcd7ad36f261b0[m
Author: Sergey Komisarchik <komisarchik@media-saturn.com>
Date:   Wed Oct 19 22:36:57 2016 +0300

    merge warmup and leaking tests into one with customizable overflow rate

[33mcommit fda5404b97289656866ee9f6be34d9a8be0e6c5a[m
Author: Sergey Komisarchik <komisarchik@media-saturn.com>
Date:   Wed Oct 19 22:10:27 2016 +0300

    cached perf fix

[33mcommit 750e75520cb5d546a667e7186092dfeeb93acedd[m
Author: Sergey Komisarchik <komisarchik@media-saturn.com>
Date:   Wed Oct 19 14:10:47 2016 +0300

    message template cache perf

[33mcommit 0c8f64611758e6611cb806ec9a3d2dbb03f192b3[m
Merge: 09092ea8 340d2085
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Oct 12 11:46:25 2016 +1000

    Merge pull request #821 from 304NotModified/304NotModified-codecov.io
    
    Added codecov.io integration

[33mcommit f68f470e50a0058e7fdc98c455304cbd86469cfd[m
Author: Artur Krajewski <krajek.dev@gmail.com>
Date:   Mon Oct 10 21:52:58 2016 +0200

    Tests for array and dictionary not exceeding limit.

[33mcommit 3417ac8bae8ec7f7c790f14c7ffdb28b985ea0dd[m
Author: Artur Krajewski <krajek.dev@gmail.com>
Date:   Thu Oct 6 09:10:44 2016 +0200

    Simplified tests for payload limiting.

[33mcommit 5e5fa770a7fd84f0caaf542b2345fbe6842c162c[m
Author: Artur Krajewski <krajek.dev@gmail.com>
Date:   Thu Oct 6 08:49:52 2016 +0200

    Basic tests for collection limiting.

[33mcommit 09092ea8c5793cdd11dc0bc724cc171fbb84e230[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Oct 6 09:53:41 2016 +1000

    A more informative and welcoming README (#873)
    
    A more informative and welcoming README [Skip CI]

[33mcommit b9128ab2c02b934168a9f0d3f483365311fea8db[m
Merge: 8828af83 f29518b7
Author: Artur Krajewski <krajek.dev@gmail.com>
Date:   Wed Oct 5 21:58:57 2016 +0200

    Merge branch 'dev' into payload-limiting

[33mcommit 8828af83e5b172a1884646a161a070cd7e9f8779[m
Author: Artur Krajewski <krajek.dev@gmail.com>
Date:   Wed Oct 5 21:57:24 2016 +0200

    Limits of collection and dictionaries length.

[33mcommit f947c17e8cad313f839bba7500874b7d5d859504[m
Author: Artur Krajewski <krajek.dev@gmail.com>
Date:   Wed Oct 5 10:54:55 2016 +0200

    Removed private from Stringify.

[33mcommit f29518b7642d3174130b14a921d3dfeb7703ed02[m
Merge: cded9919 bb1a5504
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Oct 5 15:39:17 2016 +1000

    Merge pull request #866 from krajek/payload-limiting
    
    Payload limiting

[33mcommit cded991931e5928ed02beb666a0bd9de37854cf7[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Oct 5 15:37:04 2016 +1000

    Dev version bump - payload limiting is coming in [Skip CI]

[33mcommit dd6f01d7f6f2cfdf064fbba02d100143186299ac[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Oct 5 12:20:04 2016 +1000

    Update changelist for latest releases [Skip CI]

[33mcommit c2f883bca329fd713771c92e47bad6fcbb63ebc4[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Oct 5 11:44:58 2016 +1000

    Link to the Stack Overflow tag [Skip CI]

[33mcommit 26f175abd8bbd1a7c57768849771a824201f7d15[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Oct 5 11:37:34 2016 +1000

    Dev version bump [Skip CI]

[33mcommit 673049a4a56b834f95425d90e2e43c64b4e74ebd[m
Merge: 668c31da 4bd48d4d
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Oct 4 10:42:41 2016 +1000

    Merge pull request #871 from wengfatt/patch-1
    
    Update RestrictedSink.cs (fixes #870).

[33mcommit 4bd48d4d51bcc5b40f4972abf2a52d62f64bfab2[m
Author: wengfatt <wengfattpang@yahoo.com>
Date:   Mon Oct 3 17:25:01 2016 -0700

    Update RestrictedSink.cs
    
    Implement IDisposable

[33mcommit bb1a55045aa64790fb5b79f56f2974ae58dcb4b5[m
Author: Artur Krajewski <krajek.dev@gmail.com>
Date:   Wed Sep 28 17:19:37 2016 +0200

    Test for object NOT limited with default destructuring mode.

[33mcommit d283c117ee03b83567100b8a09c648765c915f51[m
Author: Artur Krajewski <krajek.dev@gmail.com>
Date:   Wed Sep 28 16:54:32 2016 +0200

    In default destructuring mode strings are not truncated.
    
    But they are when they are captured or when they are stringified.

[33mcommit 8cd082eb1567b5eac7d55c46ae8960a7b8d7ea22[m
Author: Artur Krajewski <krajek.dev@gmail.com>
Date:   Wed Sep 28 15:27:04 2016 +0200

    Rewording of exception for max string length validation.

[33mcommit 0b92e59a7e79c991c416414c3c683543c74d3e21[m
Author: Artur Krajewski <krajek.dev@gmail.com>
Date:   Wed Sep 28 15:24:27 2016 +0200

    Default maximum string length int.MaxValue to avoid breaking changed.

[33mcommit e07294af3c5bc0cfe3c428e99b62320a930d292c[m
Author: Artur Krajewski <krajek.dev@gmail.com>
Date:   Sat Sep 24 08:03:26 2016 +0200

    Test for invalid string limit.

[33mcommit 71f9df533782ba04715ecb827447ae6621371cb4[m
Author: Artur Krajewski <krajek.dev@gmail.com>
Date:   Sat Sep 24 07:54:52 2016 +0200

    Removed $ from test of string truncation.

[33mcommit 9efea23d6e31ebfd3b341829ddc7355f1bcd8e67[m
Author: Artur Krajewski <krajek.dev@gmail.com>
Date:   Sat Sep 24 07:50:34 2016 +0200

    Truncating plain object.

[33mcommit 37203f696538fa9ff653f020017a5a9563ac96ef[m
Author: Artur Krajewski <krajek.dev@gmail.com>
Date:   Sat Sep 24 07:18:11 2016 +0200

    Test for limiting length of simple string.

[33mcommit 680589ba5450eac17f54252db3ed0582cceb4657[m
Author: Artur Krajewski <krajek.dev@gmail.com>
Date:   Fri Sep 23 22:41:25 2016 +0200

    Truncation of stringified objects.

[33mcommit 138bdb091bf08c4555f187e9d9f50f63afaf0409[m
Author: Artur Krajewski <krajek.dev@gmail.com>
Date:   Fri Sep 23 22:24:49 2016 +0200

    Test fo case of not truncating string.

[33mcommit 6afcb8ca97d732e8391245e2d830cf0721e74e34[m
Author: Artur Krajewski <krajek.dev@gmail.com>
Date:   Fri Sep 23 22:21:24 2016 +0200

    Using … character when truncating string.

[33mcommit 79ab6b068bd745df21b9835375855e20aab2f611[m
Author: Artur Krajewski <krajek.dev@gmail.com>
Date:   Fri Sep 23 18:35:16 2016 +0200

    Simple trimming of strings, according to conf(default 1000).

[33mcommit 668c31da9cad6e7cd0625d3ab02307609deb98f0[m
Merge: 32e0c957 f1d9dcae
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Sep 20 09:03:06 2016 +1000

    Merge pull request #858 from goenning/fix-852
    
    Fix for issue #852

[33mcommit f1d9dcae5cbee4c59465306b3e3dd212bb134a5c[m
Author: Guilherme Oenning <--name>
Date:   Mon Sep 19 12:02:05 2016 -0300

    some minor nitpicks

[33mcommit 32f14e1c5b86a4f0e7d60b80e31cf848e839afcf[m
Author: goenning <me@goenning.net>
Date:   Fri Sep 16 19:42:35 2016 -0300

    code refactor as suggested

[33mcommit ed6f59f17fc5e9ec34fca806e24f04d9fddfd0c7[m
Author: goenning <me@goenning.net>
Date:   Fri Sep 9 18:33:41 2016 -0300

    fix #852

[33mcommit 32e0c9578db720add74720bceb62bd46967694c4[m
Merge: e8ed41dc 4aa4ae64
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Sep 8 12:24:24 2016 +1000

    Merge pull request #856 from nblumhardt/fix-customformatter
    
    Fix #855 - use ICustomFormatter

[33mcommit e8ed41dcd9dd05e3ab33212532c19a34a86d51df[m
Merge: 2013990a 56b81e63
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Sep 8 08:45:28 2016 +1000

    Merge pull request #840 from nblumhardt/f-remoting45
    
    Ensure the per-AppDomain LogContext property name is unique across processes

[33mcommit 2013990aa56cbf3c7d2d9a50c051e8fa14c6860d[m
Merge: f70b8fc2 fcfd3129
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Sep 8 08:45:16 2016 +1000

    Merge pull request #841 from nblumhardt/dev
    
    Support `audit-to` in key-value and thus in <appSettings> settings

[33mcommit 4aa4ae6408cc5afe85a8b691e7e9f3512d4688f2[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Sep 8 08:43:20 2016 +1000

    Fix #855 - use ICustomFormatter

[33mcommit fcfd3129db259d0ce182e22ac07f95b9296aa90a[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Aug 31 09:34:32 2016 +1000

    Added feature, so minor version bump

[33mcommit bb510c969e2347daf010c9adc8e5a91d311a58b5[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Aug 31 09:31:20 2016 +1000

    Support `audit-to` in key-value and thus in <appSettings> settings.

[33mcommit 56b81e6368cefbe115b090839383a39aeb95ffcf[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Aug 31 07:50:54 2016 +1000

    Fixes #835 - ensure the per-AppDomain LogContext property name is truly unique (across processes as well as within one).

[33mcommit f70b8fc27bb62635f9ea95e2034a092383b4859e[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Aug 29 18:57:59 2016 +1000

    Dev version bump [Skip CI]

[33mcommit 7560d2b00af05206cb141bb7439cf29a5fe2ea39[m
Merge: 356156e3 9a47ac08
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Aug 29 08:13:21 2016 +1000

    Merge pull request #836 from nblumhardt/f-asynclocal4.6
    
    Use AsyncLocal rather than LogicalCallContext on .NET 4.6

[33mcommit 9a47ac0883d69bb9ad772cd6f8c5e1b2e6916871[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Aug 28 07:34:57 2016 +1000

    #835 - use AsyncLocal rather than LogicalCallContext to back LogContext on .NET 4.6+ to avoid remoting issues

[33mcommit 356156e30c903a309a4b63b588e65850786181d9[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Aug 26 16:17:36 2016 +1000

    Dev version bump [Skip CI]

[33mcommit ff63c8e8d93c714d5c68bf1bbf99b5300b15234f[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Aug 26 15:56:41 2016 +1000

    Missing `readonly` modifier

[33mcommit 1eeb972613294ca49f4fc45490ec746c2aad31c7[m
Merge: ffb7ce4d d48a13c3
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Fri Aug 19 21:35:42 2016 +1000

    Merge pull request #830 from nblumhardt/f-bench
    
    Updated benchmarks

[33mcommit d48a13c3ea90ce15b5c5226ad1fdc2d10ea59f56[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Aug 19 17:17:46 2016 +1000

    Added some benchmark results for historical reference

[33mcommit bc445014fae8d4a93db5800a80a32bc00ea86859[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Aug 19 17:11:13 2016 +1000

    Updated (micro) benchmarks

[33mcommit ffb7ce4d80abfc32cbc37edc161fff8c2a57e3aa[m
Merge: 2d88aa6e 3089b5f1
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Aug 16 07:53:07 2016 +1000

    Merge pull request #826 from nblumhardt/f-audit
    
    Audit-style logging

[33mcommit 3089b5f16d514be931b9df9c742dbde93c11e627[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Aug 16 07:36:54 2016 +1000

    Bump dev version to 2.2 (features added)

[33mcommit 2d88aa6eae7cfbe774dbe8eab19a7f8ceef33b79[m
Merge: 35933530 9c99a930
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Aug 12 19:32:25 2016 +1000

    Merge pull request #827 from nblumhardt/fix-travis
    
    Attempt to fix build under macOS

[33mcommit 9c99a9302d427a99de4ee12905595979efd64a44[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Aug 12 19:25:05 2016 +1000

    Disable the osx target in .travis.yml

[33mcommit d8cd24777a04dd0af9715e5a3b81aa2dde0d3131[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Aug 12 19:03:42 2016 +1000

    Having a shot at @joshka's suggested workaround for macOS build failures

[33mcommit 59259baa71bf3668ad1940e593ad1582c4b0f2a3[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Aug 12 17:29:52 2016 +1000

    Additional exception behavior tests and fixes

[33mcommit d8cee48b086fa4f3c8c1e1e8c8ca230c5b0c3c16[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Aug 12 16:28:35 2016 +1000

    Consistent XML doc messages

[33mcommit f0ceed51a3e9cec1ac8ded0281bb2bccadb66ed8[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Aug 12 16:27:23 2016 +1000

    Exception message punctuation

[33mcommit a12e4c7b41923a79ee94f189d72e9fcc09fcd1b2[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Aug 12 16:24:36 2016 +1000

    Support for audit logging (propagated exceptions) #820

[33mcommit 35933530ed68e3b96e035e9b7aa2e6cafee34f2a[m
Merge: 68bd3195 d1b45347
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Aug 11 14:20:04 2016 +1000

    Merge pull request #819 from nblumhardt/f-deprecateextensionpoints
    
    Deprecate virtual extension points of JsonFormatter (WIP)

[33mcommit 340d20854c1bcacc1d8b14f01bd429c932e4d5f9[m
Author: Julian Verdurmen <304NotModified@users.noreply.github.com>
Date:   Sat Aug 6 16:25:50 2016 +0200

    Added codecov.io integration

[33mcommit d1b45347559bdc6ccee633aefeec8d152b94a71e[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Jul 28 12:19:00 2016 +1000

    Include PR link in deprecation comment

[33mcommit f82e3d585354f29848e3425bab9a7a480dae0d74[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Jul 28 12:14:10 2016 +1000

    Deprecate virtual extension points of JsonFormatter in preparation for cleaning up this class

[33mcommit 68bd319531d8cab568beb4812bbe25b41297bea9[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Jul 26 14:39:11 2016 +1000

    Dev version bump [Skip CI]

[33mcommit a846885bc9613c1d4b312897296f3ae6a4f98f22[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Jul 26 14:37:41 2016 +1000

    Updated changelist [Skip CI]

[33mcommit c7d5e028f7b7118baa4f8cc54fffb3ce765743f2[m
Merge: 1ad746e3 9e617875
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Jul 23 09:57:12 2016 +1000

    Merge pull request #816 from nblumhardt/dev
    
    Fixes #815 - increasing level through overrides

[33mcommit 9e617875c3b3dcfec2c2d65f9991823ff9cad3f3[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Jul 23 09:43:06 2016 +1000

    Test method naming

[33mcommit 86911e851a1946236a674ec85492bd00d1c0f030[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Jul 22 20:03:11 2016 +1000

    Fixes #815 - increasing level through overrides

[33mcommit 1ad746e312b6a2065f9963205d1f360fe64906e9[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Jul 8 12:48:37 2016 +1000

    Minor README formatting tweak [Skip CI]

[33mcommit e797d93fc5a7baf7fa30262d26643c28a0ae8c14[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Jul 6 13:37:04 2016 +1000

    Added to 2.1 changelist [Skip CI]

[33mcommit 813388b7878ca09f451a6e4c042924743dddd1c2[m
Merge: 1413a677 a2a0fa06
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Jul 6 13:18:46 2016 +1000

    Merge pull request #799 from nblumhardt/f-objecttypesettings
    
    Select configuration methods based on matched parameter count

[33mcommit a2a0fa06bc5eff1526683555295e61117e9dff62[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Jul 6 10:07:12 2016 +1000

    Select configuration methods based on matched parameter count, not just total; fixes #798

[33mcommit 1413a6770ad4192c5e0b27ec8ca869c4e5aa9c0e[m
Merge: 147d401c f3896bae
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Jul 5 19:17:55 2016 +1000

    Merge pull request #779 from adamchester/capture-additional-params-named
    
    Capture additional params (named) not in template

[33mcommit f3896baed8aeb8307d56a4ecd463eeb487b69aa4[m
Author: Adam Chester <adamchester@gmail.com>
Date:   Tue Jul 5 18:22:35 2016 +1000

    Split matched and additional named params,
    
    which makes the logic slightly easier to digest, and maybe even slightly faster.

[33mcommit 603046a4ed73fe80c33bd1eef152666bd0fd325e[m
Author: Adam Chester <adamchester@gmail.com>
Date:   Sun Jun 19 12:06:10 2016 +1000

    Implement capturing additional properties in named templates,
    
    using __[index] naming.

[33mcommit 876447f9f7a9317b3296adc3cdf340eec99e87ee[m
Author: Adam Chester <adamchester@gmail.com>
Date:   Sun Jun 19 12:05:32 2016 +1000

    Failing tests describe capturing additional parameters,
    
    only for message templates with named properties.

[33mcommit 147d401c37b7ddf1316d94b5ac5099f44ffcab83[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Jul 5 14:43:26 2016 +1000

    Update changelist for dev build [Skip CI]

[33mcommit 9317926ae82ce6924deb35bcfbc320b3e581eb9e[m
Merge: 3709df67 574ae907
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Jul 5 14:40:31 2016 +1000

    Merge pull request #782 from adamchester/destructure-predicate
    
    Provide `ByTransformingWhere` for more complex transformations

[33mcommit 3709df676a1877e47339431fc044b8a8a3384c79[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Jul 5 14:40:18 2016 +1000

    Bump minor `dev` branch version to 2.1 (new features incoming) [Skip CI]

[33mcommit c45efd378d41543336603ccc85ad3c84a01e3367[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Jul 5 09:51:33 2016 +1000

    Corrected source project folder name matching

[33mcommit 36714938794ef6b778a8256129a6c679a7ab4884[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Jul 5 09:41:03 2016 +1000

    Alternative TestDummies project targeting

[33mcommit bc7c519f56183439a0b283e48767dcfa155272c3[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Jul 5 09:27:32 2016 +1000

    Re-enable publishing [Skip CI]
    
    This will be overwritten with the next merge from dev.

[33mcommit bfba01c80593d08b73f63361f6ce7204250c8d25[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Jul 5 09:26:33 2016 +1000

    Temporarily disable publishing to "fix" build
    
    No new package is required, but don't want to leave the build sitting on red.

[33mcommit efd247864384289938d6ed0ea1ee2e52df51377a[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Jul 5 09:22:12 2016 +1000

    Common build script, updated release tagging key.

[33mcommit f480b5ddcf1a464c596d1b2a678c3927dd3ec154[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Jun 28 08:35:01 2016 +1000

    Added 2.0 guide/release notes [Skip CI]

[33mcommit 9fe59d9126d61def67e88c20fc7818c4133189e5[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Jun 28 08:27:37 2016 +1000

    Update package version for next prerelease series

[33mcommit c9f527a3e522505740da693710b30d1ec2921322[m[33m ([m[1;33mtag: v2.0.0[m[33m)[m
Merge: 7adec78e b2da98c0
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Jun 28 08:22:59 2016 +1000

    Merge pull request #783 from serilog/staging-2.0
    
    2.0 Release

[33mcommit b2da98c002ed95f92501a447f77fdc052869fdb1[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Jun 28 08:08:14 2016 +1000

    Specify exact (2.0.0) version

[33mcommit 670dc6b5cd0b9f8ae6aea3ff08dd13c7ba660ea4[m
Merge: a55f49cb 408238ab
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Jun 26 20:55:38 2016 +1000

    Merge pull request #789 from adamchester/dotnet-cli-preview2
    
    dotnet CLI preview2 and .NET Core RTM

[33mcommit 408238ab9607eaa3ae2c77437655278add5b4c38[m
Author: Adam Chester <adamchester@gmail.com>
Date:   Sun Jun 26 14:54:35 2016 +1000

    CLI preview2-003121

[33mcommit 0d081f5d6c8ef7d902bd7a7f5b80b24d9f288bf0[m
Merge: e7f7cdd5 96537a4b
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Jun 26 09:55:57 2016 +1000

    Merge pull request #780 from adamchester/update-resharper-file
    
    Add resharper auto-added setting

[33mcommit a55f49cbc0229f0f29554353c3dd1682e4693125[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Jun 21 12:01:18 2016 +1000

    Update changelist for 2.0

[33mcommit a9d122bd4ddac4e88572d143108d684da88d00a9[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Jun 21 11:12:59 2016 +1000

    Include changelist in solution

[33mcommit e7f7cdd558a9f8eb9c333f57308503bd5a2f56a6[m
Merge: b7587298 66c73176
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Jun 21 08:26:54 2016 +1000

    Merge pull request #781 from nblumhardt/f-objecttypesettings
    
    Support setting values of interface types where a fully-qualified type name is supplied

[33mcommit 574ae9073c7a361feedf040645b0af7a852bdefc[m
Author: Adam Chester <adamchester@gmail.com>
Date:   Mon Jun 20 14:52:42 2016 +1000

    Provide `ByTransformingWhere` that accepts a predicate based on the `Type` of object.

[33mcommit 66c73176b07b3fb1a1f8731fe506d44727fe9d31[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Jun 20 14:50:06 2016 +1000

    Fix error when target type is not loadable

[33mcommit 67bfa3fcd05b63d4f22b0ba5bf66406de1ffb0ea[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Jun 20 14:34:10 2016 +1000

    Support setting values of interface types where a fully-qualified type name is supplied

[33mcommit 96537a4b3ca77313e0d48fe621e67f1b1a87eb47[m
Author: Adam Chester <adamchester@gmail.com>
Date:   Mon Jun 20 13:01:42 2016 +1000

    Add resharper auto-added setting

[33mcommit b758729856d49a4817f20528ef75b6e166b57b3f[m
Merge: 81ba6b3c cee46e7f
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Jun 20 09:27:43 2016 +1000

    Merge pull request #778 from adamchester/capturing-tests
    
    Cover property capturing with better tests

[33mcommit 81ba6b3cbc6fef3305f388254075254d63c4160b[m
Merge: 4d15d2c7 3b55d2db
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Jun 20 09:14:53 2016 +1000

    Merge pull request #733 from DmitryNaumov/remoting-issue
    
    Prevent LogContext.Enrichers serialization when doing cross-domain calls

[33mcommit 3b55d2dbcefefc97fe0db37258195190bb4a9d5b[m
Author: Dmitry Naumov <dimon-naumov@yandex.ru>
Date:   Sun Jun 19 19:32:52 2016 +0300

    Use per AppDomain slot name, so other domain won't accidentally unwrap object reference.

[33mcommit 1324aa00792592e3d86f2b583200b11b0290729f[m
Merge: a7b5f745 4d15d2c7
Author: Dmitry Naumov <dimon-naumov@yandex.ru>
Date:   Sun Jun 19 15:29:31 2016 +0300

    Merge remote-tracking branch 'refs/remotes/serilog/dev' into remoting-issue

[33mcommit a7b5f7455315ac4296ddc28e2e296d5585178bf8[m
Author: Dmitry Naumov <dimon-naumov@yandex.ru>
Date:   Sun Jun 19 13:20:50 2016 +0300

    Use ObjectHandle to wrap value stored in CallContext.
    Remove LogContext.PermitCrossAppDomainCalls/Suspend.

[33mcommit cee46e7fc6b0e92c07f6eb41f4aea734b3a71b2c[m
Author: Adam Chester <adamchester@gmail.com>
Date:   Sun Jun 19 10:00:03 2016 +1000

    Cover property capturing with better tests,
    
    Only `ScalarValue` and `SequenceValue` are covered.

[33mcommit e0a08ec1bf4fe434308869cd3840a4dd740e1cac[m
Author: Adam Chester <adamchester@gmail.com>
Date:   Sun Jun 19 09:47:21 2016 +1000

    Add basic support for LogEventProperty structural equality,
    
    The 'LogEventPropertyStructuralEqualityComparer` and `LogEventPropertyValueComparer` currently only support `ScalarValue` and `SequenceValue`. If the comparer encounters a `StructureValue` or `DictionaryValue` it throws `NotImplementedException`.

[33mcommit 4d15d2c78b719700a0667680a78948d8a152a4cb[m
Merge: ecd6e13f 3cbf04dd
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Jun 19 07:57:57 2016 +1000

    Merge pull request #776 from adamchester/faster-template-ctor-props
    
    Improve message template construction performance

[33mcommit 3cbf04dda085b9693335f3cdcea91c2253479387[m
Author: Adam Chester <adamchester@gmail.com>
Date:   Sun Jun 19 07:48:41 2016 +1000

    Remove explicit 'private' to be consistent

[33mcommit 2077978e2d7361bcfdefd34ff75340260a52a7e6[m
Author: Adam Chester <adamchester@gmail.com>
Date:   Sat Jun 18 15:36:37 2016 +1000

    Use object[] as input, slightly faster

[33mcommit d4789e1467556ed0123e92adba3ef125f00321cf[m
Author: Adam Chester <adamchester@gmail.com>
Date:   Sat Jun 18 15:10:26 2016 +1000

    MessageTemplate: Extract PropertyToken elements faster

[33mcommit ecd6e13fd7cd9d93be90994607a5d2351afba7ce[m
Merge: dd539eb2 7b1671c5
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Fri Jun 17 15:29:47 2016 +1000

    Merge pull request #772 from nblumhardt/f-visitor
    
    LogEventPropertyValue visitor implementations

[33mcommit dd539eb27f2e031ac3d5468dcbe557d7aaa0a013[m
Merge: ab5ecb0b 4544ebd4
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Fri Jun 17 15:14:59 2016 +1000

    Merge pull request #775 from nblumhardt/f-tagging
    
    Add tagging/GitHub releases support to build via AppVeyor

[33mcommit 4544ebd4a56fb8ebcba2e26e23bae3adfd114f07[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Jun 17 14:57:02 2016 +1000

    Add tagging/GitHub releases support to build via AppVeyor; closes #683

[33mcommit ab5ecb0b5c08f3760ab9420b649a249756767ffc[m
Merge: a5a38843 3d353c8d
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Jun 15 15:55:30 2016 +1000

    Merge pull request #769 from merbla/RC2-perf-tests
    
    Revive Performance Tests for RC2

[33mcommit a5a3884345b2393622f5fb661d1b24887c335b28[m
Merge: 2324bcbc d3894461
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Jun 15 15:54:21 2016 +1000

    Merge pull request #774 from nblumhardt/f-levelformatting
    
    Switch from ,n level width specifiers to :tn

[33mcommit 3d353c8dc174c88b637da9808e2478151b90c7d1[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Wed Jun 15 08:12:05 2016 +1000

    Moved to isolated perf test scripts

[33mcommit d3894461ecbedd7dead9214e11c41949039a0e57[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Jun 14 21:38:25 2016 +1000

    #765 - switch from ,n level width specifiers to :tn

[33mcommit 9d2b472c31640138de9e082fd892769b4ce7d6cd[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Tue Jun 14 11:32:01 2016 +1000

    Removed explicit private declarations.

[33mcommit 7b1671c558475769f17b76874cc1ac72b20ed8e4[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Jun 14 10:29:28 2016 +1000

    Allow the default _typeTag property name to be overridden, or the property omitted

[33mcommit 50a46d0f819394df0d0e3730f980bf4366ecb990[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Jun 13 13:54:01 2016 +1000

    Lazy-allocating version; if no rewrite takes place, the whole tree is unchanged.
    
    Maintains zero allocations except for the iterator that is required when enumerating elements of a `DictionaryValue`.

[33mcommit cd36d7a698be17bfd65ccb71c35621e92c3ac0d1[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Jun 13 10:40:39 2016 +1000

    `Serilog.Data` visitor type for dealing with `LogEventPropertyValue` data
    
    Adds a JSON formatter based on this, as a step towards moving sinks away from `JsonFormatter`, which has too many responsibilities and an awkward inheritance-based customisation model.
    
    Adds a rewriter type for modifying (immutable) `LogEventPropertyValue` structures, also based on the visitor. No concrete implementation included, but an example is used in the tests that points the way towards eventual use of this in a payload-size limiting capacity.

[33mcommit 2324bcbc71abab9d0255ae2544c5ec29835e4f11[m
Merge: 2b548007 465d6d5d
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Jun 13 08:47:30 2016 +1000

    Merge pull request #770 from Suchiman/dev
    
    Remove template parser redundancy and nano optimizations

[33mcommit 465d6d5dc2beef2bbd759cc350b2b8b0a7305565[m
Author: Suchiman <robinsue@live.de>
Date:   Sat Jun 11 15:31:33 2016 +0200

    remove redundancy and nano optimizations

[33mcommit ad6dc3a9484c58d3201403438cb8ee2ca020a215[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Sat Jun 11 18:58:25 2016 +1000

    Moved to build perf tests only.

[33mcommit ba8ac46a3cb02c7a3cb80912f0aa88d64553f5c2[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Sat Jun 11 17:11:34 2016 +1000

    Build file changes to run perf tests

[33mcommit a2c2f38e825138e0ab4f903525c577069730ef8d[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Sat Jun 11 16:19:58 2016 +1000

    Renamed test files and added LogEvent and parsing tests.

[33mcommit 1f6bb21b89a86c28be5c0dcb46a68089ef3f9518[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Sat Jun 11 09:46:16 2016 +1000

    Added FromLogContext PushProperty.

[33mcommit 01d3dff7a5bf66301df82e20960122d1f8943e7f[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Sat Jun 11 09:14:51 2016 +1000

    Initial RC2 perf tests for MinLevel and ForContext

[33mcommit 2b548007123d516d709f0d297087e961b51816cd[m
Merge: 4ffa138e ca2010fa
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Tue Jun 7 14:39:31 2016 +1000

    Merge pull request #767 from nblumhardt/f-testprojectcleanup
    
    Remove the old/unused test projects

[33mcommit ca2010fab297839ad8ed77b778a784fa193bd7c4[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Jun 7 14:20:38 2016 +1000

    Remove the old/unused test projects

[33mcommit 4ffa138edc45303648f547c564b1f3c8dd387ef2[m
Merge: 2aed1d85 998f33da
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Jun 3 07:41:30 2016 +1000

    Merge pull request #764 from khellang/stuff
    
    Some cleanup from @khellang

[33mcommit 998f33da0da35cb1b71a83a90b1b22937e6460f8[m
Author: Kristian Hellang <kristian@hellang.com>
Date:   Thu Jun 2 10:33:42 2016 +0200

    Some cleanup

[33mcommit 2aed1d85ac3096f1a0e84eafe84199e2b7bf5582[m
Merge: b229889c cbfadba5
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Jun 1 09:22:28 2016 +1000

    Merge pull request #761 from nblumhardt/f-leveloverride
    
    MinimumLevel.Override()

[33mcommit cbfadba55e6dc621ae1f194772a6e2095fffb1b1[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Jun 1 08:48:54 2016 +1000

    Nitpick, get rid of the empty string literal :-)

[33mcommit b229889cc208a7740732f8f4416b8cbb45964f7d[m
Merge: 5078c07f b14c77f1
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Jun 1 08:15:23 2016 +1000

    Merge pull request #763 from khellang/build-once
    
    Don't run build twice

[33mcommit b14c77f139226e010468b761f69d6391790d8292[m
Author: Kristian Hellang <kristian@hellang.com>
Date:   Wed Jun 1 00:13:51 2016 +0200

    Don't run build twice

[33mcommit 5078c07f336a06ef2e5cc09194ba98b4a9155517[m
Merge: 3f6e6405 08608f61
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Jun 1 08:03:16 2016 +1000

    Merge pull request #760 from merbla/setup-travis-build
    
    New Travis Build

[33mcommit 08608f612b4d43ddcb6f99554eeb46d58b9a77f2[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Tue May 31 18:54:22 2016 +1000

    Updated README with correct year(s).

[33mcommit 10b7db1423ffcb4290bad7c6913f85c67aebb1fe[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Tue May 31 18:52:22 2016 +1000

    Removed the target framework on lib build.

[33mcommit bd19528940f045d0d5ade2b3e1e35e9f94746c1f[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue May 31 15:34:53 2016 +1000

    MinimumLevel.Override(); closes #754

[33mcommit 6ddc5ba0cc58f4df51d7a45342810ae069873ef9[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Tue May 31 12:31:29 2016 +1000

    Corrected table structure

[33mcommit 00b0730086096f5efa6be9100515a7cef7256876[m
Merge: 24754202 3f6e6405
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue May 31 09:58:59 2016 +1000

    Merge branch 'dev' of https://github.com/serilog/serilog into f-leveloverride

[33mcommit c61644fa5841c61eaf4e52535406a7c25471206f[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Tue May 31 08:45:17 2016 +1000

    Updated readme with travis badges.

[33mcommit c1a5841d22387051cbd8fa3eecf9c60695103476[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Tue May 31 08:38:57 2016 +1000

    Travid build and reverted to loop in build.sh.

[33mcommit 3f6e64056160f853311a1462b2ec8c227005d75e[m
Merge: e8449cc4 28e87c8b
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Mon May 30 16:04:16 2016 +1000

    Merge pull request #758 from nblumhardt/f-selflog-action
    
    Enable(action) for SelfLog

[33mcommit 28e87c8bcb48a53b6d342aa1bed49f4c0e15c4a4[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon May 30 13:49:40 2016 +1000

    Use 'messages' consistently to describe entries written to SelfLog

[33mcommit 58b9064fc5b020b8cc1193d43d4d39714c182e9a[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon May 30 12:36:07 2016 +1000

    Fixes #731 - evented SelfLog

[33mcommit 834eae6ed67cb8ab1933b23ed6530cc4a93c6e2e[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon May 30 12:12:24 2016 +1000

    WIP

[33mcommit e8449cc4757f2c24f1d53ff5c253b96a5221f7f9[m
Merge: 37344913 028da6ce
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu May 26 15:22:24 2016 +1000

    Merge pull request #756 from merbla/build-updates
    
    Bash script for dotnet core.

[33mcommit 3734491397493a7a6d1d8366d7f7fdbb14732835[m
Merge: 2b45b91a 4736691b
Author: Kristian Hellang <kristian@hellang.com>
Date:   Thu May 26 02:33:44 2016 +0200

    Merge pull request #751 from nblumhardt/f-factorymethods
    
    Add BindMessageTemplate() and BindProperty() to ILogger

[33mcommit 028da6ce840f2edbb27c213b984e1088b63b6a6f[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Thu May 26 10:07:02 2016 +1000

    Bash script for dotnet core.

[33mcommit 2b45b91a63ad7987a133e980551e4f979796a6e1[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu May 26 09:46:41 2016 +1000

    Explicit version specified in appveyor setup script

[33mcommit 4736691b7a0cd574e178c24a61caa8a9196a6a61[m
Merge: ba596542 9c235717
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu May 26 09:10:05 2016 +1000

    Pulled upstream changes

[33mcommit 247542023af765db042a4ea0db1ee2ef493d575f[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu May 26 09:03:22 2016 +1000

    WIP

[33mcommit 9c23571787780b1fc8ff2ac20f942e3a8923bec6[m
Merge: cbb93e5d 2abc8d48
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu May 26 09:02:13 2016 +1000

    Merge pull request #750 from nblumhardt/f-forcontextallocs
    
    Optimize ForContext() by removing allocations

[33mcommit cbb93e5d4cc007e01e75956b78c330efad3c3ca5[m
Merge: eacdf648 19e2a073
Author: Kristian Hellang <kristian@hellang.com>
Date:   Wed May 25 11:49:08 2016 +0200

    Merge pull request #752 from nblumhardt/f-loggergenerics
    
    Add allocation-saving generic overloads to ILogger

[33mcommit 19e2a0732b0c4c3fa9bf307e3b7acfa16ce14469[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon May 23 14:09:18 2016 +1000

    Avoid allocations in the 0-argument case also.

[33mcommit 3259f943843262fa3e7163b260e44e2faee7f910[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon May 23 13:36:07 2016 +1000

    Add allocation-saving generic overloads to ILogger - #585

[33mcommit ba596542b186100aa0fba51cc93587027cb7ef9a[m
Merge: 3a973f0b eacdf648
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon May 23 12:43:46 2016 +1000

    Merge branch 'dev' of https://github.com/serilog/serilog into f-factorymethods

[33mcommit 3a973f0ba1e79bb9230a02f968a0541d434f6e89[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon May 23 12:43:39 2016 +1000

    Adds BindMessageTemplate() and BindProperty() to ILogger #746

[33mcommit 2abc8d4875de13e535f71429211a20712b6dc17f[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon May 23 11:42:22 2016 +1000

    Add the new ForContext() overload to Log.

[33mcommit 8f9aa4ecc4ddfe5def084475996677f6bd1f38ae[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon May 23 11:20:17 2016 +1000

    Optimize ForContext() by removing allocations; breaking change due to new method on ILogger.

[33mcommit eacdf648f2803ec4accea4f0abb6808587db5b3a[m
Merge: a19533b7 72bdc1b6
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon May 23 09:56:44 2016 +1000

    Merge pull request #749 from merbla/dotnet-build-script-change
    
    Corrected script path to match new structure in dotnet/cli

[33mcommit 72bdc1b6bd85666bc60c227df162c110628515da[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Mon May 23 09:37:58 2016 +1000

    Corrected script path to match new structure in dotnet/cli

[33mcommit a19533b723c039bf2bd4d59d5bcc7d0dc4a4ec6b[m
Merge: c2f6d747 00e83ee8
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Mon May 23 08:01:47 2016 +1000

    Merge pull request #747 from nblumhardt/dev
    
    Removes a redundant `ToArray()` call from `ForContext()`

[33mcommit 00e83ee81a90e17bef9d66420c479774205ee51d[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri May 20 20:24:10 2016 +1000

    Removes a redundant `ToArray()` call from `ForContext()`

[33mcommit c2f6d747ca1b8b6405b7f6a40e4f68fc4ace9872[m
Merge: 03c8dfb6 22e28032
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Tue May 17 12:42:55 2016 +1000

    Merge pull request #742 from nblumhardt/dev
    
    Code/test port to .NET Core RC2

[33mcommit 22e28032b93525ff5de88e43edfaf9ad550688c6[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue May 17 12:28:55 2016 +1000

    Specify configuration when running tests

[33mcommit c2076aa3b20138d3e92d25c2237f9af5536d2891[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue May 17 12:18:47 2016 +1000

    Build binaries for tests to consume

[33mcommit 5e7eaf1b61231b2e3c04028931dd1af92b9c5b92[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue May 17 12:09:46 2016 +1000

    Try default CLI install params

[33mcommit bcd1c739ee4d4180f2efc47a31fa5a8c8aa6db91[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue May 17 11:59:32 2016 +1000

    Specify the preview1 tools version; fix XML doc comment errors

[33mcommit 84d40a17045d8b3ebd5f9eb7defdd866be43aee4[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue May 17 11:45:40 2016 +1000

    Use 'latest' for .NET CLI version...

[33mcommit 574b17394387812adaaeda7d1338a55b0c02c1c6[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue May 17 11:39:27 2016 +1000

    Install script typo..

[33mcommit d9a279e69702dc3bce5121ee543a88007db1742c[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue May 17 11:34:23 2016 +1000

    Goodbye, gitter webhook (for now)

[33mcommit 6d31aca1579e4ae4ffd2d9f4fc4e4b2f1a7128ff[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue May 17 11:33:15 2016 +1000

    YML update

[33mcommit ab4d3c8454c1818de2d94fce26eb7a5fac517241[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue May 17 11:30:32 2016 +1000

    Attempt installing .NET CLI from the appveyor install script; c/o https://github.com/dotnet/cli/issues/2113

[33mcommit 2b53a7a291dda9e0a5aa3417da42198e389d4171[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue May 17 11:10:19 2016 +1000

    Updated to dotnet build

[33mcommit 60d4f8ff2ea9b2ffbb328d537bfa52951c6e3a8d[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue May 17 10:16:06 2016 +1000

    Discover all tests, even the skipped one

[33mcommit 975bddd3f266f61c09fd664f89296941b6e5ad5e[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue May 17 09:52:25 2016 +1000

    Code/test port to .NET Core RC2

[33mcommit 03c8dfb6ba4c3528b5cd458ba2351dd8f43d0028[m
Merge: 928dab49 74319ecc
Author: Kristian Hellang <kristian@hellang.com>
Date:   Thu May 5 14:19:48 2016 +0200

    Merge pull request #734 from nblumhardt/f-moveappsettings
    
    Remove Serilog.Settings.AppSettings from 2.0

[33mcommit 74319ecc77a48ae5fc6020b4f9e0bf7f29fc5688[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu May 5 20:31:07 2016 +1000

    Remove Serilog.Settings.AppSettings, which is now at serilog/serilog-settings-appsettings.
    
    Also re-enabled KeyValuePairSettingsTests, excluding some duplication to let us scope back the enricher dependencies a bit.

[33mcommit 1a66d179cab7c2e0a3dcd6cc7ef23a4c080d2f78[m
Author: Dmitry Naumov <dmitry.naumov78@yandex.ru>
Date:   Thu May 5 12:03:47 2016 +0300

    Attempting to fix AppDomain tests

[33mcommit 55cb11fd996d7adb18eb5e4621af76970fe2fcfc[m
Author: Dmitry Naumov <dmitry.naumov78@yandex.ru>
Date:   Thu May 5 11:16:05 2016 +0300

    Prevent LogContext.Enrichers serialization when doing cross-domain calls

[33mcommit 928dab497e84ebf28243f57390a269d64813d280[m
Merge: 0d2dac54 4869b03a
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon May 2 18:20:27 2016 +1000

    Merge pull request #728 from adamchester/refl-not-scalar
    
    Allow destructuring policies to transform reflection types

[33mcommit 4869b03aba25ee3301b594df3c9dffccced88fc8[m
Author: Adam Chester <adamchester@gmail.com>
Date:   Mon May 2 17:03:21 2016 +1000

    Allow destructuring/transforming reflection types,
    
    by changing ReflectionTypesScalarConversionPolicy to ReflectionTypesScalarDestructuringPolicy, custom IDestructuringPolicy implementations can then destructure or transform reflection types. Reflection types aren't very 'scalar' in the sense that they can be transformed in many useful ways.

[33mcommit 0d2dac54849eb1c7380cf347cab0b4f7e91e6cf1[m
Merge: 32127146 f464c460
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Sat Apr 30 07:07:57 2016 +1000

    Merge pull request #727 from nblumhardt/f-internalize-scalarconversions
    
    Internalize IScalarConversionPolicy, since no API allows one to be plugged in

[33mcommit f464c460ce094bd79f09be5fd1815e5a91a5fcf0[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Apr 29 16:06:52 2016 +1000

    Internalize IScalarConversionPolicy, since no API allows one to be plugged in

[33mcommit 32127146a394353eb3a26583b5a2ec5b0c404f25[m
Merge: 2f6cf15a 0bc3b217
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Apr 20 14:00:11 2016 +1000

    Merge pull request #719 from nblumhardt/dev
    
    Make the return value of `LoggerConfiguration.CreateLogger()` concrete

[33mcommit 2f6cf15aca3a96adbf40a1e839675a8d8eee0abe[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Apr 20 08:39:49 2016 +1000

    Removed unused method, removed explicit private/internal modifiers, avoided an unneeded ToString() call when supported alignment is specified.

[33mcommit 2f721d824cdfeebcfae3df73aa80572b6e5ddf13[m
Merge: e0e0ca8a 7fc443e9
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Apr 20 08:22:45 2016 +1000

    Merge branch 'iamkoch-pre-dev' into dev

[33mcommit 7fc443e9bce35f1658423b7e975e2787d0797b1f[m
Author: Antony Koch <ant@iamkoch.com>
Date:   Tue Apr 19 12:14:47 2016 +0100

    Switch from dictionary to multi dimensional array as lookup for fixed width level strings

[33mcommit 26a7d89a826970ec604e70889cd26c2c2424822f[m
Author: Antony Koch <ant@iamkoch.com>
Date:   Tue Apr 19 09:15:55 2016 +0100

    few more formatting tweaks for variable log level width

[33mcommit 0bc3b217bd5dc7d66af3a7c169e3bfbfd2f8fc91[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Apr 19 07:54:30 2016 +1000

    Removed obsolete test

[33mcommit 459dffd1ead39ef0e07f95800927d12ee00a009f[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Apr 18 16:15:51 2016 +1000

    Make the return value of `LoggerConfiguration.CreateLogger()` concrete so that disposal requirement is communicated

[33mcommit 023d57c282dcb5ddedee9401c68743a713fa0c8c[m
Author: Antony Koch <ant@iamkoch.com>
Date:   Tue Apr 5 08:22:06 2016 +0100

    Update to fixed-width levels based on Nick's feedback.

[33mcommit 3a86846e9c50ab1b2857bb26aa9f69616ccb70f4[m
Author: Antony Koch <ant@iamkoch.com>
Date:   Mon Apr 4 20:07:27 2016 +0100

    add fixed width functionality for log event output.

[33mcommit e0e0ca8a07e2db2bb8a461e191296181bd62ecbc[m
Merge: ee68faec 05d17d58
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Mar 19 08:59:29 2016 +1000

    Merge pull request #700 from merbla/remove-enrichers
    
    Removed enricher projects (moved to individual repositories).

[33mcommit 05d17d58f87756e7dc36c699d3265df5e0027983[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Fri Mar 18 13:20:26 2016 +1000

    Removed enricher projects.

[33mcommit ee68faec21694d27da94555811bb587a64b6c8d0[m
Merge: 57adabec 94d3b46f
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Mar 18 10:52:31 2016 +1000

    Merge pull request #699 from merbla/remove-remaining-sinks
    
    Removed remaining sinks and specific sink tests.

[33mcommit 94d3b46fe652ad1007aa86428edff20c68d2b6b3[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Thu Mar 17 14:28:33 2016 +1000

    Removed remaining sinks and specific sink tests.

[33mcommit 57adabec1b139d42c3b9e5f80283c47d1102e679[m
Merge: 81c35701 f3387962
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Mar 16 17:09:37 2016 +1000

    Merge pull request #698 from merbla/move-sinks-with-no-test-impact
    
    Move sinks with no test impact

[33mcommit f33879627d6ce7ea34b3113abd8a09e936023cf4[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Wed Mar 16 16:24:12 2016 +1000

    Removed trace sink.

[33mcommit 7c03a24120f91a98c8a9cfeba9d8c8d197a960c0[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Wed Mar 16 15:51:20 2016 +1000

    Removed the colored console sink.

[33mcommit 81c3570123a646690dc8446f6e380d866f2e9558[m
Merge: 1996a4ec b1f44300
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Mar 15 16:16:48 2016 +1000

    Merge pull request #696 from merbla/move-console-sink
    
    Removed console sink to new repo at https://github.com/serilog/serilog-sinks-console

[33mcommit b1f443005059eab3d6dc81ed1da19783401efbcd[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Tue Mar 15 15:50:14 2016 +1000

    Removed console sink to new repo at https://github.com/serilog/serilog-sinks-console.

[33mcommit 1996a4ec33fa8ff3f63ec34cbb7540f5864b6de1[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Mar 15 13:39:57 2016 +1000

    Added Gitter badge

[33mcommit 4dd3d6816e14ca82cd6c97bb5db22e56507d25fb[m
Merge: b1752657 2812a75c
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Mar 14 15:26:52 2016 +1000

    Merge pull request #693 from blachniet/patch-1
    
    Add rager releases badge to README.md

[33mcommit 2812a75c17e2b864737d3b81cb984a2b0c64b962[m
Author: Brian Lachniet <blachniet@gmail.com>
Date:   Sun Mar 13 21:02:18 2016 -0400

    Add rager releases badge to README.md
    
    [Rager.io](http://rager.io) is a service that makes it easy for developers to keep up-to-date with projects they use. When you publish a new releases of this project on NuGet, users that have signed up to receive notifications on [Rager.io](http://rager.io) will receive an email notification with a link to the new release.

[33mcommit b1752657d4045a6b0a9e5123dc8b6b07d78a841b[m
Merge: e08dab1c 23530243
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Mar 13 13:15:23 2016 +1000

    Merge pull request #689 from jonorossi/clscompliant
    
    Mark assemblies CLSCompliant again

[33mcommit 23530243e3628b880b8db92edcf7cf952b3989eb[m
Author: Jonathon Rossi <jono@jonorossi.com>
Date:   Sat Mar 12 01:05:31 2016 +1000

    Mark assemblies CLSCompliant again

[33mcommit e08dab1c8a731c5ef5cf4c0e350b4d76bfca50d4[m
Merge: 3f8b8fb4 e83c17a5
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Mar 2 07:46:09 2016 +1000

    Merge pull request #623 from DmitryNaumov/periodicbatching-contention
    
    Replace state transitions under lock with Interlocked operations

[33mcommit e83c17a5012c0a8ec15c128847d35fe76c284875[m
Merge: 4da9b3bd 3f8b8fb4
Author: Dmitry Naumov <dmitry.naumov78@yandex.ru>
Date:   Mon Feb 29 10:05:17 2016 +0300

    Merge branch 'dev' of https://github.com/DmitryNaumov/serilog into periodicbatching-contention

[33mcommit 4da9b3bd7d785de4068e82ef7b0c76aaa0ef6aae[m
Author: Dmitry Naumov <dmitry.naumov78@yandex.ru>
Date:   Mon Feb 29 10:02:57 2016 +0300

    Back to lock-based version

[33mcommit 3f8b8fb449aa39308f23422fab9cde0d2b63c912[m
Merge: 2a28183f 7fcee49e
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Feb 26 09:16:25 2016 +1000

    Merge pull request #678 from Pvlerick/issue-649
    
    Fixing ColoredConsoleSink to prevent printing empty properties

[33mcommit 7fcee49e5b5ea181f56c1510f8580afed7602a16[m
Author: Philippe <pvlerick@gmail.com>
Date:   Tue Feb 23 07:49:22 2016 +0100

    Adding guard clause to prevent empty properties being printed

[33mcommit 2a28183f9526c98e200f38d9d9acd3de4912d9e0[m
Merge: 30785fe6 25775717
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Feb 22 10:28:56 2016 +1000

    Merge pull request #677 from merbla/packaging-issues-for-new-projects
    
    Change to Build.ps1 to use separate package folders

[33mcommit 25775717cb8bf31d01873493500e2c771ad6df60[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Mon Feb 22 10:19:10 2016 +1000

    Clean up and refactoring.

[33mcommit 39f7f4601c1cd792e34f5e85171b1f50757b717b[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Mon Feb 22 10:13:52 2016 +1000

    Moved to use per project output folders for packaging.

[33mcommit 30785fe68c0d5a0a45df56365ca68aa3382f7f36[m
Merge: afa17f1d 3aee6eba
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Feb 22 08:12:01 2016 +1000

    Merge pull request #674 from merbla/split-enrichers
    
    Moved enrichers to new projects.

[33mcommit 3aee6eba0137bde3d58b88273385c8032c20363b[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Sun Feb 21 14:49:22 2016 +1000

    Removed extra target for Serilog.Enrichers.Process

[33mcommit 9fab1e74b53c4619ebbb68beabe7a31b89dc7de7[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Sun Feb 21 14:45:33 2016 +1000

    Clean up and comments from @nblumhardt
    
    Package icons, package descriptions, if defs.

[33mcommit 69cc2e33e0b21dfbc2738292ef44d60df8628b8a[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Sat Feb 20 08:27:48 2016 +1000

    Moved enrichers to new projects. Serilog.Enrichers.Environment contains UserName and Machine Name. Serilog.Erichers.Thread contains ThreadId. Serilog.Enrichers.Process contains ProcessId.

[33mcommit afa17f1d1a782f99e33fcf283d9f91a6c82c8796[m
Merge: b3e3db91 37ba2f6f
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Fri Feb 19 15:57:33 2016 +1000

    Merge pull request #670 from nblumhardt/f-appsettingspkg
    
    Moved AppSettings support to Serilog.Settings.AppSettings

[33mcommit b3e3db91717b2d6a65b318518315a17e2f46ba7e[m
Merge: c995e4b0 7981b389
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Fri Feb 19 06:38:28 2016 +1000

    Merge pull request #669 from nblumhardt/f-2xcleanup
    
    Move `FromLogContext` to `LoggerEnrichmentConfiguration`, tidy some ifdefs

[33mcommit c995e4b06eba2e8e9e5cb50fee88ff122caa8e92[m
Merge: 505f6c8d a42a1607
Author: Kristian Hellang <kristian@hellang.com>
Date:   Thu Feb 18 11:42:45 2016 +0100

    Merge pull request #671 from nblumhardt/f-loggertypes
    
    A tiny internal change - make `Logger` implement `ILogEventSink` explicitly

[33mcommit a42a1607e0a8a2b1d07b0c6a2d28e546fe768f84[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Feb 18 12:27:10 2016 +1000

    A tiny internal change - make `Logger` implement `ILogEventSink` explicitly so that it's clearly an implementation detail and not something we depend on accidentally.

[33mcommit 37ba2f6ff7c5aee638418bc82a458a560aefc8eb[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Feb 18 11:41:32 2016 +1000

    Removed the `sinks` solution folder so the structure still matches the filesystem.

[33mcommit 7981b38923a3351113db608ceae646c5bde5f073[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Feb 18 11:39:14 2016 +1000

    Fixed identification of FromLogContext configuration method in Key/Value settings

[33mcommit e87b99937417700bfb9ba473b017fa104259bba4[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Feb 18 10:51:59 2016 +1000

    Moved AppSettings support to Serilog.Settings.AppSettings

[33mcommit 5699bde2ec27587e0e6cce6bda9bc3827cb926bc[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Feb 18 09:41:14 2016 +1000

    Move `FromLogContext` to `LoggerEnrichmentConfiguration`, tidy some ifdefs

[33mcommit 505f6c8d80353fe834f58884d0b88db905c82be2[m
Merge: 946f0c90 c65dc5b7
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Feb 16 17:12:39 2016 +1000

    Merge pull request #648 from colin-young/logcontext-588
    
    Enable LogContext for .Net Core.

[33mcommit 946f0c90714fb85c4763561179f34fe9b0bbe1f1[m
Merge: a601e967 1f674c35
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Feb 12 08:12:01 2016 +1000

    Merge pull request #656 from Pvlerick/issue-650
    
    Adding buffered boolean param to FileSink and RollingFileSink

[33mcommit 1f674c352c8a3bdb7f49e7a4a254e61971d70c06[m
Author: Philippe <pvlerick@gmail.com>
Date:   Wed Feb 10 08:15:17 2016 +0100

    Added buffered param to FileSing and RollingFileSink ctors

[33mcommit c65dc5b78f71f36fb1d38df08cf75727b6d4f15a[m
Author: Colin Young <colin.young.todo@gmail.com>
Date:   Tue Feb 9 21:49:10 2016 -0500

    Removing LOGCONTEXT feature and #ifdefs

[33mcommit b3352df0775b9fd2507a34d9db965fa72ac92c6c[m
Author: Colin Young <cyoung@patientsecure.com>
Date:   Tue Feb 9 09:57:56 2016 -0500

    Missed correcting framework in previous commit.

[33mcommit 2fd3810e487e6d80c5bf474b98569d98ef76e806[m
Author: Colin Young <cyoung@patientsecure.com>
Date:   Tue Feb 9 09:54:27 2016 -0500

    Fixing up framework specifiers and adding support for .Net Standard 1.0

[33mcommit a601e967de50614c9f8ee654c3d9de3de90e1cad[m
Merge: fb9ae29e 74cf4239
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Tue Feb 9 13:39:34 2016 +1000

    Merge pull request #659 from nblumhardt/net40-removal
    
    Fixes #635 - remove .NET 4.0 conditionals.

[33mcommit fb9ae29e7711d0177b84cf5cfc68611636cb3592[m
Merge: a224101f f363265b
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Feb 9 13:33:14 2016 +1000

    Merge pull request #654 from nblumhardt/fix-child-logger-level
    
    Less surprising "child logger" level configuration

[33mcommit 74cf42391e3a9a1f0886cbb6fb2bcc86d689f6f7[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Feb 9 13:31:32 2016 +1000

    Fixes #635 - remove .NET 4.0 conditionals.

[33mcommit f363265bec55849cb27aabbb39b8bb2eb8f61f7c[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Feb 2 07:43:51 2016 +1000

    Less surprising "child logger" level configuration
    
     - When using `WriteTo.Logger(lc => ...)` the sub-logger inherits the level configured on the parent
     - Last-in always wins when setting minimum levels even when switching between `LoggingLevelSwitch` and basic level config
     - `WriteTo.Logger` no longer ignores the passed-in level switch argument if present (consistent with other sinks)

[33mcommit f036e16d461d26370feb255a480916e707b99c05[m
Author: Colin Young <cyoung@patientsecure.com>
Date:   Thu Jan 28 19:45:36 2016 -0500

    Fixing unit tests to work with previous commit.

[33mcommit 09afb7839d9bd51ccc51b467dc37c4757350949b[m
Author: Colin Young <cyoung@patientsecure.com>
Date:   Thu Jan 28 19:36:14 2016 -0500

    Moving PermitCrossAppDomainCalls inside REMOTING #ifdef.

[33mcommit 4e9c2a38e7a2c95a0cfba4605e86305c0f96e3cf[m
Author: Colin Young <cyoung@patientsecure.com>
Date:   Thu Jan 28 08:40:12 2016 -0500

    Removing unrelated changes from enrichers.

[33mcommit 42dfa307ff62685d5929c0287f1cf35c06ae82fa[m
Author: Colin Young <cyoung@patientsecure.com>
Date:   Wed Jan 27 17:08:47 2016 -0500

    Fix naming for consistency.

[33mcommit ab37bfd2898c330e7c36d624a18746600c1a8170[m
Author: Colin Young <cyoung@patientsecure.com>
Date:   Wed Jan 27 17:03:38 2016 -0500

    Consolidating #ifdef blocks to make code easier to read/maintain.

[33mcommit 65173508411809e5d57e10b2e37d03b610057667[m
Author: Colin Young <cyoung@patientsecure.com>
Date:   Wed Jan 27 17:01:27 2016 -0500

    Adding extra context to lead to correct location in MSDN.

[33mcommit e5a4d1b2be9b7fd4af2682409987951b1b80424d[m
Author: Colin Young <cyoung@patientsecure.com>
Date:   Wed Jan 27 16:53:35 2016 -0500

    Removing unused constructor.

[33mcommit 92c0ffae2f4b460a1abf888d7724f7f686770d6a[m
Author: Colin Young <cyoung@patientsecure.com>
Date:   Wed Jan 27 12:02:45 2016 -0500

    Slight reduction in #ifdefs.

[33mcommit 8edfa166d2c6af7b4576f183ae046aaabad468af[m
Author: Colin Young <cyoung@patientsecure.com>
Date:   Tue Jan 19 16:32:38 2016 -0500

    Enable LogContext for .Net Core. https://github.com/serilog/serilog/issues/588

[33mcommit a224101fb31de4395532e566eda1ec8475add2d8[m
Merge: d208a3fc 44822569
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Jan 26 10:54:19 2016 +1000

    Merge pull request #643 from merbla/the-great-sink-migration-of-2016
    
    Migrate remaining sinks to new projects

[33mcommit 44822569f25c3eb144c950f57648d05826e0cdfa[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Tue Jan 26 10:43:07 2016 +1000

    Removed old deps for Rolling File sink

[33mcommit d5e042878be484d463a337e55752b49aa1d50039[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Mon Jan 25 09:43:18 2016 +1000

    Removed default constants from Core and move to sinks
    
    Also
    - Corrected missing Header/Copyright
    - Removed old PERIOD_BATCHING directive

[33mcommit 7e4c194542791463098523a45048261a743354f3[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Sat Jan 23 15:57:03 2016 +1000

    Removed targets not required

[33mcommit d4814a963cf30971c74bf786671e2ddd45574366[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Sat Jan 23 14:33:56 2016 +1000

    Renamed Serilog.Sinks.DiagnosticTrace to Serilog.Sinks.Trace

[33mcommit 4f297bcd345a9e8f792b76963454b5abb28794a0[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Fri Jan 22 15:36:01 2016 +1000

    Removed 5.4 targets on projects that target 5.1 from @khellang

[33mcommit 499bf1ebdde1a77e13680c1cfee3dae99aa2d4ec[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Fri Jan 22 15:33:40 2016 +1000

    Corrected issue with namespace conflict with System.IO.File

[33mcommit 9c24d46ca16382b2d964f38badf899436270ba2d[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Fri Jan 22 15:24:19 2016 +1000

    Changed namespaces to match renamed sink projects

[33mcommit 539ac6fc063ad0d976fa03a78b12fe86f875f87f[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Fri Jan 22 15:22:22 2016 +1000

    Renamed Serilog.Sinks.IOTextWriter to Serilog.Sinks.TextWriter

[33mcommit 6fa8d2d0c505bb288141f3d309a6a23d286cd70c[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Fri Jan 22 15:16:46 2016 +1000

    Renamed Serilog.Sinks.IOFile to Serilog.Sinks.File

[33mcommit db29dd1f77b99275866a83eb6e746e8a614bac6d[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Fri Jan 22 14:44:58 2016 +1000

    Corrected Wording and naming of classes from @nblumhardt
    
    - Descriptions in project.json for packages
    - Missing Logger in extension classes
    - Renamed old FullNet extension

[33mcommit 309becf73d8ede4e19e1e5103acd9122f8e5bf32[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Thu Jan 21 15:48:19 2016 +1000

    Corrected naming on Sink

[33mcommit db11f1912965fe4c2646dbd3b914a4ca1a64c40d[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Thu Jan 21 13:09:21 2016 +1000

    Clean up on targets.  Expanded to 5.4 on some sinks.  Need to set a baseline for core sinks in relation to target frameworks.

[33mcommit 9f1bad45c90c465367a942cbcac924b2ea88309e[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Wed Jan 20 15:27:58 2016 +1000

    Moved IOTextWriter to new sink project

[33mcommit 1c56af84de38b4ec289f7876253d78ea0d28a1a5[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Wed Jan 20 15:16:00 2016 +1000

    Moved Observable Sink to new project

[33mcommit 82f99b34b4eacb8d3281374126b50d21e6654efa[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Wed Jan 20 14:30:32 2016 +1000

    Moved PeriodicBatching Sink to new project

[33mcommit 5cf24776637a9df99c4f728ff9f71341805911ae[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Wed Jan 20 13:45:31 2016 +1000

    Removed redundant files

[33mcommit 7abaafca5ded90f833153075eef9049246ce9a7a[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Wed Jan 20 13:43:43 2016 +1000

    Moved colored console sink

[33mcommit 0d49b19f0d7a01e7dc63735fa2cba3fd6cdd4bfd[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Wed Jan 20 13:05:30 2016 +1000

    Moved IO Sinks to new projects. Dependencies required a combined commit

[33mcommit b3d9701bd8778390fb74967b74281da9e6a5bfcb[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Sat Jan 16 14:08:42 2016 +1000

    Extended dependencies for dotnet 5.4 for diagnostic trace sink

[33mcommit eb5bd01da89a35ac7ba95bf20fb398c01a8fba8d[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Sat Jan 16 14:01:31 2016 +1000

    Corrected description on diagnostic trace sink meta data

[33mcommit 413d81bde1ba2278d21e5b24d352270ba8974de6[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Sat Jan 16 13:55:14 2016 +1000

    Moved Serilog.Sinks.DiagnosticTrace to new project

[33mcommit d208a3fc603521b0fbecf547278f161b29260231[m
Merge: 2a632bab 98cf960c
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Jan 21 08:03:09 2016 +1000

    Merge pull request #636 from johncrn/master
    
    Enable event enrichers from config

[33mcommit 98cf960c6dfe4ea27bf52443583527851afcfad6[m
Merge: e35d82b6 54fde6c3
Author: John Crnjanin <john.crn@live.com>
Date:   Sun Jan 17 22:01:46 2016 +1000

    Merge remote-tracking branch 'refs/remotes/origin/jcrefactor'

[33mcommit 54fde6c39c08401a3b4ce9e45cb06d5f3eb80bf5[m
Author: John Crnjanin <john.crn@live.com>
Date:   Sun Jan 17 21:57:26 2016 +1000

    Event enrichers from config refactoring

[33mcommit 18cb4c09c55bb349fb80576c20de27e1d7ea6469[m
Author: John Crnjanin <john.crn@live.com>
Date:   Sun Jan 17 16:18:02 2016 +1000

    Refactored how directives are applied to loggerConfiguration

[33mcommit e35d82b62fc01c9dd1a215eee5832608f63b56f1[m
Author: John Crnjanin <john.crn@live.com>
Date:   Sun Jan 17 09:44:42 2016 +1000

    Event enrichers by settings uses extension methods
    
    Keeps syntax in settings the same as if using config object and uses
    established extension methods as entry points rather than directly
    activating the enrichers.

[33mcommit 885b26962c0dd0ffbf50a91e3c5622884050b62f[m
Author: John Crnjanin <john.crn@live.com>
Date:   Sat Jan 16 22:21:00 2016 +1000

    Enable Event Enrichers from Config
    
    Event enrichers implementing the ILogEventEnricher interface (e.g.
    ThreadIdEnricher, MachineNameEnricher, etc) can be enabled via config,
    rather than just the Property enricher as was prior.

[33mcommit 9ea4a41836bbde5e4f3ed3749d843a0a6d582df0[m
Merge: cf414a90 2a632bab
Author: John Crnjanin <john.crn@live.com>
Date:   Sat Jan 16 15:58:42 2016 +1000

    Merge remote-tracking branch 'refs/remotes/serilog/dev'

[33mcommit 2a632bab4799cae93620fee7366810405fac44b8[m
Merge: b195b43b ff447f59
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Jan 14 21:29:41 2016 +1000

    Merge pull request #622 from Pvlerick/issue-618
    
    Added function to detect anonymous types and appropriate tests

[33mcommit b195b43b62c5dd85ef2a7f74be80baf473c8e537[m
Merge: 0b7aba95 d6b322cd
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Jan 14 20:30:09 2016 +1000

    Merge pull request #630 from merbla/packagefactoring-sinks
    
    Package Refactoring - Split Console Sink to new project

[33mcommit ff447f598088093578c67cd3e52745c855b9d377[m
Author: Philippe <pvlerick@gmail.com>
Date:   Wed Jan 6 18:57:56 2016 +0100

    Added support for VB anonymous types and appropriate tests.
    
    Wheck is done in IsCompilerGeneratedType that performs fastest check first and the  uses the indexer that is faster than String.StartWith

[33mcommit d6b322cd7176d39bc684b91bac3d84f3e3d26a35[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Thu Jan 14 16:55:02 2016 +1000

    Moved to standard Serilog sink project structure where project has root namespace of Serilog.  Also moved sinks to folder structure that maps to namespace.  This should be used for other sink project ports. Finally updated logo to be sink icon.

[33mcommit 76723d80b9be997bddce734bb733154cc1beebf0[m
Author: Dmitry Naumov <dmitry.naumov78@yandex.ru>
Date:   Wed Jan 13 13:04:10 2016 +0300

    Avoid racing for first event and let it be included in the first batch

[33mcommit a2f4c1bd87c64b8923c4d94a2762781baa8fadac[m
Author: Dmitry Naumov <dmitry.naumov78@yandex.ru>
Date:   Wed Jan 13 12:16:45 2016 +0300

    Replace volatile keyword with in-place method calls

[33mcommit e0d829138bfba475e7c8575fc29c418a34819cc0[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Wed Jan 13 12:21:29 2016 +1000

    Pruned project.json a little more to remove 4.0

[33mcommit 9456057681a22fb010d1e711b713f2771127521c[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Wed Jan 13 12:05:01 2016 +1000

    Got a little eager, reverting some of the deletes

[33mcommit ec0e11e243f6fb72ee3bcb435d8529b2c29948f2[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Wed Jan 13 12:01:13 2016 +1000

    Removed 4 references

[33mcommit 0c4e9e08f548de2c53c699add17f8c04fed63274[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Wed Jan 13 11:49:16 2016 +1000

    Renamed extensions file and refined tags

[33mcommit 7023fa656542173c19d31957a0209b9ce20169f4[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Wed Jan 13 04:27:07 2016 +1000

    First round of changes suggested by @khellang

[33mcommit 36cb87539af51837577ccae594072d9cf9fe4fea[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Mon Jan 11 21:30:59 2016 -0800

    Removed console checks from reflection tests

[33mcommit fbbed833f1692e4bb514c407aa81bee86c97e934[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Tue Jan 12 15:06:31 2016 +1000

    Corrected visibility of console sink

[33mcommit e0093bc3e9264cf7ae61afc295f9fa0ed7a2a798[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Tue Jan 12 15:02:13 2016 +1000

    Initial cut of console sink as an example of porting sinks

[33mcommit 0b7aba956708105abc7873e39459982b4a4c81ce[m
Merge: b202e5e5 6018f0ef
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Mon Jan 11 18:58:22 2016 -0800

    Merge pull request #627 from nblumhardt/fix-589-sn
    
    Enable strong naming on CoreCLR builds

[33mcommit b202e5e5dcf3e0cda8e4faa236fcf1df10ed2fc1[m
Merge: 53ee481e 233dc069
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Sun Jan 10 16:31:40 2016 -0800

    Merge pull request #626 from nblumhardt/dev
    
    Use round-trippable floating point formatting in JSON

[33mcommit 6018f0efe1859bec1616f09165d7b31863fababf[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Jan 11 08:32:42 2016 +1000

    Fixes #589 - enable strong naming on CoreCLR builds (excludes tests using internal APIs)

[33mcommit 233dc0693541022467a69a43663786d228312665[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Jan 11 07:58:31 2016 +1000

    Fixes https://github.com/continuousit/seq-releases/issues/358 - use "R" round-trip specifier when formatting floating-point numbers to JSON.

[33mcommit 0cddf8b25d6e80c51733f6de68313e95ae89f380[m
Author: Dmitry Naumov <dmitry.naumov78@yandex.ru>
Date:   Thu Jan 7 14:04:39 2016 +0300

    Replace state transitions under lock with Interlocked operations

[33mcommit 53ee481eb1db7700557f87bcd3de31052cf32eb8[m
Merge: 71a65c71 01e3982e
Author: Kristian Hellang <kristian@hellang.com>
Date:   Mon Dec 28 10:49:52 2015 +0100

    Merge pull request #608 from nblumhardt/dev
    
    Attempt to fix intermittent hang during CoreCLR test runs

[33mcommit 01e3982ebf61955c5000286486d6b9aafb0b1b8f[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Dec 26 14:54:03 2015 +1000

    Mitigate two possible causes of intermittent test failure under CoreCLR
    
     - Don't .ConfigureAwait on the Task.CompletedResult that's returned from a zero-length delay (unsure why this would be an issue but *appeared* to fix test
     - Suspend SynchronizationContext when awaiting EmitBatchAsync() (this is almost certainly not the issue but appears to be a worthwhile related change)

[33mcommit 71a65c710e826e3fab68efa1da249c81c906c49d[m
Merge: d97f2b4f ad220b28
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Dec 26 11:30:04 2015 +1000

    Merge pull request #616 from khellang/new-targetg
    
    Replaced Profile259 with dotnet5.1

[33mcommit ad220b28cf27ff7990c05978b85b85e20d5a1010[m
Author: Kristian Hellang <kristian@hellang.com>
Date:   Tue Dec 22 00:23:14 2015 +0100

    Reworked ifdefs to pivot on feature rather than platform

[33mcommit 15eaf8c9b97617d8a840d88d0c6db10146064849[m
Author: Kristian Hellang <kristian@hellang.com>
Date:   Mon Dec 21 23:32:23 2015 +0100

    Added dotnet5.1 to replace Profile259

[33mcommit f91eaf900cd20a09fe0ce141af020a1fb2f05cc0[m
Author: Kristian Hellang <kristian@hellang.com>
Date:   Mon Dec 21 23:09:52 2015 +0100

    Removed dnx451, should be covered by net45

[33mcommit d97f2b4f6fbd2afe42f3ccebfeef1e431eda239b[m
Merge: 73c36555 e97b3c02
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Dec 23 20:32:32 2015 +1000

    Merge pull request #617 from khellang/csharp-6-take2
    
    Kill some trailing whitespace, update to C# 6

[33mcommit e97b3c028bdb28e4430512b84dc2082e6f98dcc7[m
Author: Kristian Hellang <kristian@hellang.com>
Date:   Wed Dec 23 11:28:35 2015 +0100

    :fire: some trailing whitespace, update to C# 6

[33mcommit 73c3655539b15b7e5b1e4e357e7e1fe05a27ed4c[m
Merge: 1218d62a b57f3507
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Dec 3 08:30:04 2015 +1000

    Merge pull request #600 from khellang/rc1-update
    
    Update to RC1

[33mcommit b57f350721a9d3285ec0d9ce461558b2861fa7b7[m
Author: Kristian Hellang <kristian@hellang.com>
Date:   Wed Dec 2 18:38:50 2015 +0100

    DNXCORE50 -> DOTNET5_4

[33mcommit ff385b4e6f71929c093d1d47844269208ccdfd09[m
Author: Kristian Hellang <kristian@hellang.com>
Date:   Wed Dec 2 17:54:03 2015 +0100

    Attempt at fixing failing tests

[33mcommit 3e62d5a5e3393586fc8807186efb48e92346fd69[m
Author: Kristian Hellang <kristian@hellang.com>
Date:   Wed Dec 2 17:46:05 2015 +0100

    Adjusted indentation in project.json

[33mcommit 6fa3d33b19bc3208f002ecbae0a6c99a7d485a52[m
Author: Kristian Hellang <kristian@hellang.com>
Date:   Wed Dec 2 17:42:15 2015 +0100

    Removed project.lock.json

[33mcommit 66b82cd0a6b634132543ce163720a44bf7ab3b3c[m
Author: Kristian Hellang <kristian@hellang.com>
Date:   Wed Dec 2 17:41:55 2015 +0100

    Removed CommonAssemblyInfo, it's not used anymore

[33mcommit 40a1bdc3cfd7b90d5960b44a0b28fb05d3da635c[m
Author: Kristian Hellang <kristian@hellang.com>
Date:   Wed Dec 2 17:41:20 2015 +0100

    Update to RC1

[33mcommit 1218d62a60a650ba4c56267e777e7568375a00d8[m
Merge: 22e9da97 2e3470bb
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Nov 21 07:02:44 2015 +1000

    Merge branch 'daveaglick-issue561' into dev

[33mcommit 2e3470bb7a69e564c3f3c450b8dff3334141e3d3[m
Merge: 22e9da97 01fa69d7
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Nov 21 07:02:22 2015 +1000

    Merge branch 'issue561' of https://github.com/daveaglick/serilog into daveaglick-issue561
    
    # Conflicts:
    #       test/Serilog.Tests/LoggerConfigurationTests.cs

[33mcommit 22e9da97724576fa329f4a87eea85f64e5fb762d[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Nov 20 22:24:13 2015 +1000

    Remove dev-v2 from the README

[33mcommit 7110a494dfb401d117a046ed2f3eafca2291e10b[m
Merge: 7adec78e e5f38584
Author: Kristian Hellang <kristian@hellang.com>
Date:   Fri Nov 20 13:18:16 2015 +0100

    Merge pull request #591 from serilog/dev-v2
    
    Move dev branch to "v2" (VS2015/.NET Core)

[33mcommit e5f38584142a2331ed5752ba73ef57850f45444b[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Nov 20 21:50:12 2015 +1000

    Don't resume the PortableTimer background task on the captured context that initiated it.

[33mcommit 8ae52849f1b8a08a3a8360a063ff9cf4a3a913c8[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Nov 20 21:43:44 2015 +1000

    Make sure the issue isn't related to PortableTimer and SynchronizationContext.

[33mcommit 2b2c93e0c69830e0d8a592dd8c9e8d019da3d091[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Nov 20 21:13:48 2015 +1000

    Running out of ideas - could the CI machine really be _that_ slow?

[33mcommit faf573cac2793cc58e278b133c39b1abce63667e[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Nov 20 20:43:43 2015 +1000

    Revert attempt to fix test via timings; looks like the CI build itself may be the problem.

[33mcommit 3fa241589c4d5203eb01668f59a2d6fc35d24006[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Nov 20 08:16:02 2015 +1000

    Test runner is slow.

[33mcommit cf6f7c21786177791af756db8d64ee5b3fdb10f5[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Nov 20 07:45:31 2015 +1000

    Fix failing test caused by a bad merge.

[33mcommit 6d659dccdf06784d676e887becc07240ef1e103e[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Nov 18 08:07:47 2015 +1000

    Port `EnvironmentUserNameEnricher` to the v2 project system.

[33mcommit bfb2d94748b6e719811acf7fe035fe29931098b5[m
Merge: 55572f6e 7adec78e
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Nov 18 07:57:38 2015 +1000

    Merge branch 'dev' into dev-v2
    
    # Conflicts:
    #       src/Serilog.FullNetFx/Serilog.FullNetFx.csproj
    #       src/Serilog/Formatting/Json/JsonFormatter-net40.cs
    #       src/Serilog/LoggerConfiguration.cs
    #       src/Serilog/LoggerConfigurationFullNetFxExtensions.cs
    #       src/Serilog/Settings/AppSettings/AppSettingsSettings.cs
    #       test/Serilog.Tests/LoggerConfigurationTests.cs
    #       test/Serilog.Tests/Serilog.Tests.csproj
    #       test/Serilog.Tests/app.config

[33mcommit 55572f6efeeaa0ff6ea8793a5e6e23a028dbb59f[m
Merge: 365924fe c8329808
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Wed Nov 18 07:25:33 2015 +1000

    Merge pull request #557 from nblumhardt/dev-v2
    
    .NET Core/v2 updates

[33mcommit 7adec78e7b67a2d908eaef7062e50251cf7d149d[m[33m ([m[1;33mtag: v1.5.14[m[33m)[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Nov 13 21:19:51 2015 +1000

    Changelist - missed a pending PR in the last build.

[33mcommit 838fc77bc8b6d5986667582af4f2d5cdfe3c490e[m
Merge: b4615aee a9beaf4f
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Nov 13 21:17:47 2015 +1000

    Merge pull request #572 from inosik/jsonformatter/custom-writers
    
    Allow to add and override custom writers

[33mcommit b4615aee4aa265e2c98327335eef228ed198e9fc[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Nov 13 20:39:57 2015 +1000

    Changelist

[33mcommit cd4e58b7b01d8c143e3ca65a56341ae043abedd8[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Nov 13 20:31:54 2015 +1000

    Fixes #579 - when the `RollingFileSink` fails to open a file, make sure to check for `null` in subsequent attempts to close it.

[33mcommit a9beaf4fbe3973cb4f00bdd11ce6ecb55e9121a9[m
Author: Ilja Nosik <ilja.nosik@outlook.com>
Date:   Sun Nov 8 12:29:57 2015 +0100

    Add docs

[33mcommit 7453a65d5c6073fdfe82732f0b061e24f85a9de2[m
Author: Ilja Nosik <ilja.nosik@outlook.com>
Date:   Sun Nov 8 12:12:57 2015 +0100

    Allow to register literal writers - fixes #567
    
    This allows to register and to override already registered writers for
    literal values in subclasses of the the JsonFormatter class.

[33mcommit e2ae59d7717f47aa52abaaf40f393b978e9415fb[m
Merge: 16285cff b2ac00e3
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Nov 9 07:49:38 2015 +1000

    Merge pull request #550 from caioproiete/add-support-custom-prefix-appsettings
    
    Add support for multiple sets of AppSettings using custom prefixes

[33mcommit b2ac00e38b03fce3e7ab18ad7694ad95ba191a6a[m
Author: Caio Proiete <caio.proiete@gmail.com>
Date:   Sat Nov 7 22:05:50 2015 -0800

    AppSettings: Switch to overload and non-default param so that the change is non-breaking

[33mcommit 6f96a18952a9dd512cdc94cf924c4b0121f94937[m
Author: Caio Proiete <caio.proiete@gmail.com>
Date:   Thu Nov 5 15:20:32 2015 -0800

    Custom prefix implements same rules of dev-v2 and update test to work with #562

[33mcommit 6b0a88938c568e2c7f11f85e1e2a5ca8bc314b01[m
Author: Caio Proiete <caio.proiete@gmail.com>
Date:   Sun Oct 25 16:21:36 2015 -0300

    Add support for multiple sets of AppSettings using custom prefixes. Resolves #430

[33mcommit 01fa69d714d177d11d45db76c33bc3cae7451333[m
Author: Dave Glick <dave@daveaglick.com>
Date:   Sat Nov 7 20:29:45 2015 -0500

    LoggerConfiguration.CreateLogger() now throws if called more than once, resolves #561

[33mcommit 365924fe7ecac87c38f4ac4f8f4a60bcec192f37[m
Merge: 2f00d6b4 b2efd07b
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Nov 8 09:21:35 2015 +1000

    Merge pull request #564 from merbla/dev-v2
    
    Apply #562 to dev-v2

[33mcommit 2f00d6b46a5a093f026e1df8bae920825d1a404b[m
Merge: 3221a705 3a698bba
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Nov 8 08:28:39 2015 +1000

    Merge pull request #568 from johnduhart/566-flush-log
    
    #566 - Try and set the log source if it's specified

[33mcommit 16285cff1292d70a99b44ba20b7a8b47f1012c65[m
Merge: 599e6437 14c070bb
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Nov 8 08:14:12 2015 +1000

    Merge pull request #570 from caioproiete/add-environment-username-enricher
    
    Add EnvironmentUserNameEnricher + unit test

[33mcommit 14c070bbf9d7d93bb222d98dc578e2979b0a6719[m
Author: Caio Proiete <caio.proiete@gmail.com>
Date:   Fri Nov 6 16:39:58 2015 -0800

    Add EnvironmentUserNameEnricher + unit test

[33mcommit 3a698bba69a3624bdd2f8f6be64ac5ccb2aab20d[m
Author: John Du Hart <john@johnduhart.me>
Date:   Thu Nov 5 20:25:39 2015 -0500

    Try and set the log source if it's specified
    
    serilog/serilog#566

[33mcommit b2efd07ba2116f078dd77551abd47531d1180ab6[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Tue Nov 3 21:30:50 2015 +1100

    Added stub sink to fulfil test execution

[33mcommit f9694d31ed682fcf2c8ea88e1552652037602cd1[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Tue Nov 3 21:13:43 2015 +1100

    Apply #562 to dev-v2
    
    Takes changes from #562 and applies to dev-v2.

[33mcommit 599e64378ca64348cb499650ab8baeb409b0138e[m
Merge: 22bfbc57 aaa3628c
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Tue Nov 3 20:01:17 2015 +1000

    Merge pull request #562 from horsdal/unconfigured-logger-should-be-off
    
    use silent logger when creating a logger and there are no sinks in the configuration

[33mcommit aaa3628c4301bf662a55e3bce6549acb1a152c04[m
Author: Christian Horsdal <c.horsdal@gmail.com>
Date:   Mon Nov 2 23:57:55 2015 +0100

    exit early from when creating logger with no sinks are configured

[33mcommit e50dcc84c2dc97a2d0aa162950a5daaa448c6759[m
Author: Christian Horsdal <c.horsdal@gmail.com>
Date:   Mon Nov 2 23:06:36 2015 +0100

    use silent logger when creating a logger and there are no sinks in the configuration

[33mcommit c8329808dc340343dc7189a7adb94a567b9962ee[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Nov 1 11:15:59 2015 +1000

    IconURL.

[33mcommit 9228bdeb0c1936abd1a65b9d6c38fbb012e77202[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Nov 1 09:50:33 2015 +1000

    Give the slower async scheduling mechanism in the portable timer enough room to kick in during testing.

[33mcommit 4b8f81eb1744bb96a4ec3cf7150869809c1190f9[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Nov 1 09:42:26 2015 +1000

    Revert strong naming on CoreCLR, causes problesms for InternalsVisibleTo and testing.

[33mcommit 0fe5211680e046c1c771c9f48d5157484e790ed1[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Nov 1 09:32:02 2015 +1000

    Try to make sure assembly identities are the same (strong named) when targeting sub-setted profiles. SN in general needs more thought here, it'd be great to avoid it for `dotnet` but ideally not at the expense of splitting the assembly identity :-|

[33mcommit 98dbe218e828d3ce1d068dca20cc820ee99d6ebd[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Nov 1 09:08:17 2015 +1000

    Back to Caio's original settings prefix design :-) ... Specify `null` for `"serilog"`, specify `"myApp"` for `"myApp:serilog:"`.

[33mcommit 1b0263443d4de9f6373942b452d4e790ba9e2509[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Nov 1 08:56:30 2015 +1000

    Generate Serilog.dll and Serilog.Tests.dll for testing purposes during build; failing test still can't locate files on disk, but think this is closer.

[33mcommit 4bf4f67aed90b66273bae43e5dc60b4b44fb34b9[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Oct 31 23:00:18 2015 +1000

    Fixed build and test.

[33mcommit e12f75e324097d5e259f0d75d3d68ba660e285ab[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Oct 31 22:17:17 2015 +1000

    Target the dotnet TFM (tests stay in DNX)

[33mcommit 68b8835d9f926dc0e6fef7c0fc0f26a0dbf0dece[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Oct 31 21:40:00 2015 +1000

    Disallow ':' in custom setting prefixes so that ambiguities like the possibility of using "serilog:write-to" as a prefix are not allowed.

[33mcommit 10417b6a19538d63a8af95f8a1e2611567cf6596[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Oct 31 21:33:50 2015 +1000

    Merge/enable tests from serilog/serilog#551.

[33mcommit 9ba747dea32fc344fe50aedd378ffb2f673db3b8[m
Merge: 215b7d3d 63d2f8de
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Oct 31 21:26:25 2015 +1000

    Merge branch 'dev-v2' of https://github.com/serilog/serilog into dev-v2

[33mcommit 63d2f8de5fe055b9126aae953bdec60f911c4f7f[m
Merge: 10eb817b 3221a705
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Oct 31 21:25:58 2015 +1000

    Merge pull request #551 from caioproiete/add-support-custom-prefix-appsettings-v2
    
    Add support for multiple sets of AppSettings using custom prefixes (in v2)

[33mcommit 215b7d3df6d87de5bb0ad58ddbb385694755ec5c[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Oct 31 21:23:31 2015 +1000

    Port PeriodicBatchingSink to DNXCORE50.

[33mcommit f9942e6df535200e026cb2cce0a0ba1511d059fb[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Oct 31 20:11:55 2015 +1000

    Update to beta8, reenable skipped tests.

[33mcommit 082c25feec0401dbc23a00f6113f36c8838cb11b[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Oct 31 17:27:05 2015 +1000

    Throw when attempting to Subscribe or Unsubscribe from a disposed observable sink.

[33mcommit 34a1aaeb336b23c898a9caa662a1b37fd2229bde[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Oct 31 17:24:41 2015 +1000

    Switch to nameof(arg) throughout the solution.

[33mcommit f658a3e68a8364fe138488c86de6419b7335bd1f[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Oct 31 17:24:18 2015 +1000

    Minor thread safety fix on unsubscribe from observable sink.

[33mcommit ab1f8e260533f6ce386843463f3f69a17a00ea1c[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Oct 31 17:15:50 2015 +1000

    Update the copyright year.

[33mcommit 3221a70567c0399d711d15b7d784ac0b192c46e2[m
Author: Caio Proiete <caio.proiete@gmail.com>
Date:   Mon Oct 26 21:03:11 2015 -0300

    Add support for multiple sets of AppSettings using custom prefixes (in v2). Resolves #430

[33mcommit 10eb817b7d47304904af9040e1aab072714b54b7[m
Merge: d64b058f f211c2ef
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Oct 21 15:16:47 2015 +1000

    Merge branch 'dev-v2' of https://github.com/serilog/serilog into dev-v2
    
    # Conflicts:
    #       src/Serilog/Configuration/LoggerSinkConfiguration.cs
    #       src/Serilog/LoggerConfigurationFullNetFxExtensions.cs
    #       test/Serilog.Tests/Support/CollectingSink.cs

[33mcommit d64b058fb1e2eaf52ffebd7be870cc33f81e35a3[m
Merge: b53cc797 c37391f8
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Oct 21 15:12:39 2015 +1000

    Merge branch 'master' into dev-v2;
    
    # Conflicts:
    #       src/Serilog.FullNetFx/Sinks/PeriodicBatching/PeriodicBatchingSink-net40.cs
    #       src/Serilog/Serilog-net40.csproj
    #       src/Serilog/Serilog.csproj
    #       test/Serilog.Tests/Core/CopyingSinkTests.cs
    #       test/Serilog.Tests/Serilog.Tests-net40.csproj
    #       test/Serilog.Tests/Serilog.Tests.csproj

[33mcommit 22bfbc57a9bacd223ff9e276dff45bf526afedf5[m[33m ([m[1;33mtag: v1.5.12[m[33m)[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Oct 21 10:35:34 2015 +1000

    Update README.md

[33mcommit c37391f8bc31794d47ea577c02276d5f05531327[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Oct 21 10:33:20 2015 +1000

    Changelist.

[33mcommit 0a7610760434368e31af36f954f9594d7bf55611[m
Merge: dcb1a54f eeb7eb78
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Oct 21 10:30:54 2015 +1000

    Merge branch 'master' into dev

[33mcommit dcb1a54f2bd1b0fe6c4038f307835291a888e01b[m
Merge: 6fd9f818 63d2f6ef
Author: Kristian Hellang <kristian@hellang.com>
Date:   Mon Oct 12 13:47:22 2015 +0200

    Merge pull request #540 from nblumhardt/fix-532
    
    Fixes #532 - adds `WriteTo.Logger()` accepting `ILogger`

[33mcommit 6fd9f81892f6da42c2ef06f3fed77d4a717c2950[m
Merge: df7ad322 b48b9c8e
Author: Kristian Hellang <kristian@hellang.com>
Date:   Sat Oct 3 02:27:17 2015 +0200

    Merge pull request #542 from russdaygh/dev
    
    Marked assembly as CLS Compliant per #538

[33mcommit b48b9c8e487c634fe93076a95b6216eda76615b8[m
Author: Russell Day <russell.day.coder@gmail.com>
Date:   Fri Oct 2 17:20:10 2015 +0100

    Added newline at file end
    
    Added newline at file end, GitHub complained about it.

[33mcommit d6aac55ec1d36745fba6d0484c97c6cfc6e15911[m
Author: Russell Day <russell.day.coder@gmail.com>
Date:   Fri Oct 2 17:18:08 2015 +0100

    Marked assembly as CLS Compliant per #538
    
    Added ClsCompliant attribute to assembly.

[33mcommit 63d2f6efd13401f51f3835a1309d7c7734fbf6ae[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Sep 25 07:24:11 2015 +1000

    Fix the .NET 4 build.

[33mcommit c9b4e8357f8c0ed17d94e76eee5cef3387f51773[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Sep 23 07:31:39 2015 +1000

    Fixes #532 - adds `WriteTo.Logger()` accepting `ILogger`. Ensures sub-loggers are disposed when appropriate.

[33mcommit eeb7eb786e7bab12d89defde0df2928e94e059fc[m[33m ([m[1;33mtag: v1.5.11[m[33m)[m
Merge: d5fba7f0 df7ad322
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Sep 10 08:05:35 2015 +1000

    Merge branch 'dev'

[33mcommit df7ad3220b44476b9cd4f06072b0dcb797ea4dfd[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Sep 10 08:05:16 2015 +1000

    Changelist

[33mcommit 4fffe6b3a5dc40b28213d6993680cae431697d5a[m
Merge: c25c73a0 f35253da
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Sat Sep 5 15:25:31 2015 +1000

    Merge pull request #524 from nblumhardt/fix-523
    
    Exclude large strings from the internal message template cache (fixes 523)

[33mcommit f35253da2e1de385783e8f5e5290f7ea7df08451[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Sep 5 08:11:31 2015 +1000

    Fixes serilog/serilog#523 - exclude large strings from the internal message template cache
    
    Also changes the cache overflow policy to simply reset, hopefully keeping relevant items in the cache when it's outgrown via some misbehaving code.

[33mcommit d5fba7f072a408338d08fa68e2bd4637e2571f43[m[33m ([m[1;33mtag: v1.5.10[m[33m)[m
Merge: 031f85fd c25c73a0
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Sep 4 09:02:14 2015 +1000

    Merge branch 'dev'

[33mcommit c25c73a0e845da00681528b73021802610caeb5b[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Sep 4 09:00:30 2015 +1000

    Updated Changelist

[33mcommit 42645adb1b0dc2612314a4ab130959d41cd34634[m
Merge: c1cc4d8d 990ab544
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Thu Aug 27 22:10:09 2015 +1000

    Merge pull request #514 from nblumhardt/empty-batch-flowctl
    
    PeriodicBatchingSink hook for processing on ticks even when the batch is empty

[33mcommit f211c2ef03dd2c11ae245a5e35e41a91552333e1[m
Merge: b53cc797 0c8f3c27
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Thu Aug 27 21:54:48 2015 +1000

    Merge pull request #515 from nblumhardt/switchable-sinks
    
    Add a `levelSwitch` parameter to all sink configuration methods (v2)

[33mcommit 0c8f3c2707639efcb6b8eb315fd54b3246888267[m
Merge: 86439343 b53cc797
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Aug 20 08:16:34 2015 +1000

    Merge branch 'dev-v2' into switchable-sinks
    
    Conflicts:
            src/Serilog/LoggerConfigurationFullNetFxExtensions.cs
            test/Serilog.Tests/Serilog.Tests.csproj

[33mcommit b53cc797bc4a96a49912987b69a49762e12edb92[m
Merge: 7f6b9d85 c1cc4d8d
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Aug 20 08:01:26 2015 +1000

    Merge branch 'dev' into dev-v2
    
    Conflicts:
            global.json
            src/Serilog/Configuration/LoggerSettingsConfiguration-net40.cs

[33mcommit 990ab5443313f46e50a87e188ac3bf4ff296d69e[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Aug 19 20:38:25 2015 +1000

    .NET 4.0 support for `OnEmptyBatch`

[33mcommit 3c5ee48c72acc52e00f8c03db9d64dff6cc1e40a[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Aug 19 20:36:44 2015 +1000

    Adds `OnEmptyBatch` to permit batched sinks to perform periodic work on the timer thread.

[33mcommit 7f6b9d85aa40a11ac2b44de451ee8e8b2107eb23[m
Merge: a3689bda 074697f4
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Aug 19 20:20:25 2015 +1000

    Merge pull request #512 from khellang/dnx-tests
    
    Run tests on DNX

[33mcommit a3689bdad9d9de1ca843338dc4db839231ba1e70[m
Merge: 749500aa 60ff05dc
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Aug 19 08:08:40 2015 +1000

    Merge pull request #510 from khellang/dnx-build-script
    
    Added initial build script for DNX-based builds

[33mcommit 074697f4dccc42fcc13bc3e67a457bebe62b44e8[m
Author: Kristian Hellang <kristian@hellang.com>
Date:   Mon Aug 17 19:37:42 2015 +0200

    Adjusted tests for DNXCORE50

[33mcommit e741cc7f3cad0eb8be5bb05bc2176ba62068df8e[m
Author: Kristian Hellang <kristian@hellang.com>
Date:   Mon Aug 17 19:36:14 2015 +0200

    Lockfile decided to change :confounded:

[33mcommit 1516f2a34f34abe6623e9b84964cd27baec61a8e[m
Author: Kristian Hellang <kristian@hellang.com>
Date:   Mon Aug 17 19:29:58 2015 +0200

    Skip failing tests

[33mcommit 3c79859aead25ffadc84b161f06163a2fb192751[m
Author: Kristian Hellang <kristian@hellang.com>
Date:   Mon Aug 17 19:30:08 2015 +0200

    Add test command

[33mcommit e7a25e5811afb3d9b98ea5659f6174e9e1218a88[m
Author: Kristian Hellang <kristian@hellang.com>
Date:   Mon Aug 17 19:29:08 2015 +0200

    Remove [SetUp]

[33mcommit bbfded77b60e0abafba7ef7420bf7678a669dd43[m
Author: Kristian Hellang <kristian@hellang.com>
Date:   Mon Aug 17 19:31:51 2015 +0200

    Fixed asserts

[33mcommit 25f44b71d75be26a4968e74321e566f76fcdff22[m
Author: Kristian Hellang <kristian@hellang.com>
Date:   Mon Aug 17 19:31:36 2015 +0200

    Adjust InternalsVisibleTo

[33mcommit 8117627374ce64ad146172ecc83c40958af07b2d[m
Author: Kristian Hellang <kristian@hellang.com>
Date:   Mon Aug 17 16:25:05 2015 +0200

    [Test] -> [Fact]

[33mcommit 8a3f3c8658a99678de9fb6ec20d97f4f819b85fc[m
Author: Kristian Hellang <kristian@hellang.com>
Date:   Mon Aug 17 16:23:52 2015 +0200

    Removed [TestFixture]

[33mcommit 7b2f9e4e6032d4255938348030204afb55c916e3[m
Author: Kristian Hellang <kristian@hellang.com>
Date:   Mon Aug 17 16:22:01 2015 +0200

    NUnit.Framework -> Xunit

[33mcommit b5702de946732fbe63f8f5cde86c525f81671288[m
Author: Kristian Hellang <kristian@hellang.com>
Date:   Mon Aug 17 16:21:44 2015 +0200

    Added xunit to project.json

[33mcommit 0343bd36aa2d97e459837a9f7b9e85f35735ad40[m
Author: Kristian Hellang <kristian@hellang.com>
Date:   Mon Aug 17 16:18:21 2015 +0200

    Added Serilog.Tests using project.json

[33mcommit 60ff05dce1bba79cc21ec760a853f806f33a2575[m
Author: Kristian Hellang <kristian@hellang.com>
Date:   Mon Aug 17 15:33:16 2015 +0200

    Renamed Project.json -> project.json

[33mcommit 4a8c64a9dcc557a1e80f402360df6bc758c90a29[m
Author: Kristian Hellang <kristian@hellang.com>
Date:   Mon Aug 17 15:26:19 2015 +0200

    Added System.Dynamic.Runtime

[33mcommit c2b65f6705208b0958df6a9ebde48b550e81c95a[m
Author: Kristian Hellang <kristian@hellang.com>
Date:   Mon Aug 17 13:32:53 2015 +0200

    Shiny new and updated .gitignore :sparkles:

[33mcommit 10db7595f1fe7d052de830d1a703a86ccadc952a[m
Author: Kristian Hellang <kristian@hellang.com>
Date:   Mon Aug 17 12:51:44 2015 +0200

    Added initial build script for DNX-based builds

[33mcommit 749500aaf0da67dce1e9dc62999701593a1d97bc[m
Merge: 2e7673b3 187e2628
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Aug 17 21:11:13 2015 +1000

    Merge pull request #509 from khellang/fix-build
    
    Fix build

[33mcommit 86439343014e2b5b272a20587e0e3698fe83adad[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Aug 17 21:05:10 2015 +1000

    Remain binary compatible with (most) sink configuration methods in third-party assemblies.

[33mcommit 187e2628d7163701f6bd2d93bd076c868f5272af[m
Author: Kristian Hellang <kristian@hellang.com>
Date:   Mon Aug 17 12:39:07 2015 +0200

    Removed redundant compiler directives

[33mcommit 6e0b45ebb34f2d1dfb52304c3e11b7fd0cdbdbad[m
Author: Kristian Hellang <kristian@hellang.com>
Date:   Mon Aug 17 12:38:40 2015 +0200

    Automatic migration of R# settings

[33mcommit 384377cd6e8269ac0aa778e4231c09ecc06767bf[m
Author: Kristian Hellang <kristian@hellang.com>
Date:   Mon Aug 17 12:38:26 2015 +0200

    Added NuGet.config

[33mcommit 00bde6d85561c1cdc076eacb0dbcb33597432e2c[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Aug 17 20:33:21 2015 +1000

    Add a `levelSwitch` parameter to all sink configuration methods. A binary breaking change. Closes #505.

[33mcommit c1cc4d8d00055d8cabfae3ac540f3befed9f1d08[m
Merge: 45870745 972b8cba
Author: Kristian Hellang <kristian@hellang.com>
Date:   Mon Aug 17 11:31:52 2015 +0200

    Merge pull request #508 from nblumhardt/remove-vs2015-cruft
    
    Removed obsolete VS2015 solution artifacts

[33mcommit 972b8cba5f58bbf396c8c036a2943c0854c90bdd[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Aug 17 17:46:03 2015 +1000

    Some more cruft removal.

[33mcommit adaee9fe9c738bad1ab10116e5f2aeb9ab0a2feb[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Aug 17 17:42:29 2015 +1000

    Found a couple more obsolete files.

[33mcommit 9cc22e0b16b7791a39f06ae881798c0f907ffb6b[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Aug 17 17:40:27 2015 +1000

    Removed obsolete VS2015 solution artifacts

[33mcommit 4587074532f460c4ba48adc0dcfff0759f54ede6[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Jul 27 19:44:26 2015 +1000

    Tweaked the README change from the last PR

[33mcommit 34f108dea172a0865045ab4bad8c1065765f89d0[m
Merge: 031f85fd 441dc22e
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Jul 27 19:42:41 2015 +1000

    Merge pull request #497 from merbla/dev
    
    Changed to use branch specific badges

[33mcommit 441dc22e70a005f9771efa9d07325cb0ff73ab59[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Mon Jul 27 12:18:03 2015 +1000

    Removed v2 Dev

[33mcommit d0b5c916c796c9cb2c21689e2d559ff22bd9e1e2[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Sat Jul 25 09:09:18 2015 +1000

    Changed to use master specific badges

[33mcommit 031f85fd951ad00db3f10e0385d6c032dee1af1d[m[33m ([m[1;33mtag: v1.5.9[m[33m)[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Jul 21 08:10:43 2015 +1000

    Changelist

[33mcommit 543e5b1c677f07677d03bdf81ed757c72a6fffa9[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Jul 21 05:48:09 2015 +1000

    Fixes #491 - configuration method for KeyValuePairs() on .NET 4.0.

[33mcommit 2e7673b3afdcca4794476993b16959f4836b9855[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Jul 21 05:44:07 2015 +1000

    Convert to VS2015/DNX; .NET 4.0 build still causing some release build problems around async.

[33mcommit 19ed512685840c15624d2fe1532684c2e07ee4f1[m[33m ([m[1;33mtag: v1.5.8[m[33m)[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Jul 15 19:33:59 2015 +1000

    Changelist

[33mcommit f8a2fe9f0343e46347ca59cae65b5f75a2528d36[m
Merge: e2e679e1 d0e8b750
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Jul 9 19:26:11 2015 +1000

    Merge pull request #484 from justinvp/allocations
    
    Add Log overloads that help avoid array and boxing allocations

[33mcommit d0e8b75095c67636cacc154955e7bdf43a752f47[m
Author: Justin Van Patten <jvp@justinvp.com>
Date:   Wed Jul 8 07:36:39 2015 -0700

    Add Log overloads that help avoid array and boxing allocations

[33mcommit e2e679e1238e1bc3af93715024aae389b641f3bd[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Jul 7 13:13:52 2015 +1000

    Missing changes from the last commit - reset log context in tests.

[33mcommit e79a4cc58e9fa13837aef45f251dea4b8a4a4c3d[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Jul 7 13:10:45 2015 +1000

    Reset LogContext before each test in its fixture.

[33mcommit e8ed4753344720336535e990d68887aa8f8c1c5b[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Jul 7 08:14:26 2015 +1000

    One additional test for LogContext when the PermitCrossAppDomainCalls option is enabled.

[33mcommit 9be14885e2273e856c432961d10c9a16337262a8[m
Merge: 3fc65657 da70cbfd
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Jun 17 08:00:37 2015 +1000

    Merge pull request #471 from ldemidov/dev
    
    Including logging level in Trace logger

[33mcommit da70cbfd740f9189d778fe63f401ed156873ef63[m
Author: ldemidov <leviathan1337@gmail.com>
Date:   Tue Jun 16 17:13:46 2015 -0400

    Including logging level in Trace logger
    
    On Azure, there's a an option to log Trace events to a destination set
    on Azure Portal (storage tables or files).    Since Serilog was not
    including the event type in the trace log, trace messages generated by
    Serilog are not being logged.  There's also a minimum logging level
    setting in Azure that uses this event type to choose logging messages to
    log.
    
    To get this feature working properly, I updated the Trace sink to
    include the event type in the Trace message based on the Serilog
    LogEventLevel.

[33mcommit 3fc656572fc48b78e340f329bd77b2c60b4d4d6e[m[33m ([m[1;33mtag: v1.5.7[m[33m)[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Jun 12 08:14:22 2015 +1000

    Changelist

[33mcommit 390778a10259994be4c498b56a3cda482ccfe55d[m
Merge: 78373bae 23859bb2
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Jun 6 12:03:12 2015 +1000

    Merge pull request #463 from CaioProiete/enable-test-for-appsettings-env-variables-expansion
    
    AppSettings: Update the unit test that checks environment variable expansion is supported

[33mcommit 78373bae762932af45d6159da472392db14d7015[m
Merge: b9c1e672 860abe8f
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Jun 6 11:39:10 2015 +1000

    Merge pull request #467 from merbla/dev
    
    Added NuGet Badge

[33mcommit 860abe8fc4890f4abd1f1bfba1f2464e44e1882c[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Sat Jun 6 09:24:12 2015 +1000

    Added NuGet Badge

[33mcommit 23859bb2890e87f06fbf4443a6fce1ea932784e0[m
Author: Caio Proiete <caio.proiete@gmail.com>
Date:   Mon Jun 1 21:32:10 2015 -0400

    Update the unit test that checks environment variable expansion is supported

[33mcommit b9c1e672ebf0a5b16cb98bd65285c8f8ab8b6470[m
Merge: 84e6b571 8b6acf9e
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sat May 30 10:59:19 2015 +1000

    Merge pull request #456 from tsvayer/dev
    
    backport KeyValuePairSettings and AppSettings to net40

[33mcommit 8b6acf9eff804fb286064055cc5507fcabf6b21b[m
Author: Vitaliy Tsvayer <tsvayer@gmail.com>
Date:   Thu May 28 17:51:34 2015 +0300

    Remove unused extensions

[33mcommit c40cc9110263119716d2cc86661b374a7002edd6[m
Author: Vitaliy Tsvayer <tsvayer@gmail.com>
Date:   Thu May 28 17:22:32 2015 +0300

    backport KeyValuePairSettings and AppSettings to net40

[33mcommit 84e6b571be29a3c195f0fd702cb8e143cb249bb1[m[33m ([m[1;33mtag: v1.5.6[m[33m)[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun May 17 20:56:18 2015 +1000

    Generate correct assembly attributes for dev builds

[33mcommit 04637708702298a221a962c98ec0f0f55edd7438[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun May 17 20:46:25 2015 +1000

    Fixed the build and changed versioning mechanics
    
     - Builds on `master` now derive the version from the first line of `CHANGES.md`
     - Builds on `dev` use this plus `-pre-$patch`
     - Some cleanup is still needed

[33mcommit 4951a054f733188add25b51dedf771aec09830e2[m
Merge: 7382caa0 7b5040d3
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun May 17 19:51:26 2015 +1000

    Merge branch 'dev'

[33mcommit 7b5040d30044b11a902675e330411624bb51132f[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun May 17 19:51:14 2015 +1000

    Changelist

[33mcommit bf5857898b413713178c67b88bd2d4647f5ceda2[m
Merge: eaf4c977 7382caa0
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun May 17 19:48:54 2015 +1000

    Merge branch 'master' into dev

[33mcommit eaf4c9771a19eebb0697f39823df17bd1414efae[m
Merge: b9a83cbb cfd55acf
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu May 7 07:32:25 2015 +1000

    Merge pull request #442 from coding4food/dev
    
    Fixes #441 - KeyValuePairSettings.ConvertToType does not work with Nullable<System.TimeSpan>

[33mcommit cfd55acf372e3e3aea0632ce90fee12eb32a746a[m
Author: amedvedkov <amedvedkov@synterramedia.ru>
Date:   Wed May 6 15:10:06 2015 +0300

    Fixes #441 - KeyValuePairSettings.ConvertToType does not work with Nullable<System.TimeSpan>

[33mcommit b9a83cbbce10fa3a4315eb3c8bc06e138153ad31[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon May 4 21:40:22 2015 +1000

    Line up the NUSPEC description with the website tagline

[33mcommit 7382caa0422f61156c4e5af226272c4571a0a58e[m[33m ([m[1;33mtag: v1.5.5[m[33m)[m
Merge: 93be5959 e203aff7
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Apr 28 20:14:48 2015 +1000

    Merge branch 'dev'

[33mcommit e203aff7e6f373f34d2f9b2a4d38894c35864325[m
Merge: 050a3062 93be5959
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Apr 28 20:14:38 2015 +1000

    Merge branch 'master' into dev

[33mcommit 050a3062599c579c72e886c483c857c1f7288496[m
Merge: 70b785af 0ab1f9e8
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Apr 28 20:14:20 2015 +1000

    Merge branch 'dev' of https://github.com/serilog/serilog into dev

[33mcommit 93be595940aa9c54e02bb5bee316d49b2deaec72[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Apr 28 20:13:40 2015 +1000

    Changelist

[33mcommit 0ab1f9e88052c306d290ba1537f81fa58f54af58[m
Merge: 346083c5 0d9b9019
Author: Kristian Hellang <kristian@hellang.com>
Date:   Fri Apr 24 11:33:52 2015 +0200

    Merge pull request #433 from nblumhardt/dev
    
    Revert the default destructuring depth back to 10

[33mcommit 0d9b9019622a32ec0d698c5cd6b33d9715b40ed7[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Apr 24 19:06:18 2015 +1000

    Revert the default destructuring depth back to 10

[33mcommit 2463e9933aeef984530ec4da38addaed2db2a353[m
Merge: b2b9e700 70b785af
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Apr 9 21:29:00 2015 +1000

    Merge branch 'dev'

[33mcommit 70b785af854b5c654e925b97a57e7f6006579263[m[33m ([m[1;33mtag: v1.5.1[m[33m)[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Apr 9 21:27:11 2015 +1000

    Changelist

[33mcommit 346083c58140b59d223ca66ca18d3f53aa04ae0d[m
Merge: b2b9e700 a104842b
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Thu Apr 9 21:15:30 2015 +1000

    Merge pull request #422 from nblumhardt/dev
    
    Merge Serilog.Extras.AppSettings into the core, in line with #402
    
    @nblumhardt Only thoughts was namespacing/naming of `Serilog.Settings.AppSettings.AppSettingsSettings`.  It seems internal, so can be changed later.

[33mcommit a104842b0ee7ac2a97609b043883f3a88209030b[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Apr 9 17:20:39 2015 +1000

    Fix the .NET 4 build, include some additional tests

[33mcommit b87096ba193dc33eec78f62b3376a9933ac70f52[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Apr 9 07:51:02 2015 +1000

    Moved `KeyValuePairs` setting support into the portable assembly, sans environment variable expansion
    
    Environment variables are only supported in App.config files.

[33mcommit 0afe3e154c732e9e6b4f21a1089d56f608650563[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Apr 8 20:37:29 2015 +1000

    Merge Serilog.Extras.AppSettings into the core, in line with #402
    
    Moved in largely as-is, with some work remaining.

[33mcommit b2b9e700512caab4c1bf72e55b43ea84497cdc63[m[33m ([m[1;33mtag: v1.4.214[m[33m)[m
Merge: 929cdf03 fa5f0f5d
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Apr 7 21:47:01 2015 +1000

    Merge branch 'dev'

[33mcommit 929cdf0392d0eb1f5d402935408d1050faa4c519[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Apr 7 21:46:51 2015 +1000

    Changelist

[33mcommit fa5f0f5d408310f15fe7d4ccae1ffd3d7a4e7bbb[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Apr 7 21:33:31 2015 +1000

    Moved Serilog.Extras.Web to https://github.com/serilog-web/classic and Serilog.Extras.MSOwin to https://github.com/serilog-web/owin
    
    This was a trailing part of #344

[33mcommit 3f7154bd487b1dab4c4a7355e33dd28b3e3aa298[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Mar 29 19:12:35 2015 +1000

    Remove the FSharp proj from Build.ps1

[33mcommit 67e5314fa46d561c567ba6dbaffe18a950aed772[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Mar 29 18:53:59 2015 +1000

    Removed Serilog.Extras.FSharp and Serilog.Extras.DestructureByIgnoring - these are now in https://github.com/destructurama (#414).

[33mcommit 9f3a87a03e5871b7795c6dc1cf809388d36ea9f9[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Mar 29 15:28:16 2015 +1000

    Removed Serilog.Extras.Attributed, now at https://github.com/destructurama/attributed (see #414)

[33mcommit 86cae4f925e036ef5f47fe7f249212cf05dcd4b6[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Mar 29 12:51:02 2015 +1000

    Removes Serilog.Extras.Topshelf - the Topshelf project now includes this functionality (and is maintaining it more actively) so we'll publish a redirect package to it once it's on NuGet.
    
    https://github.com/Topshelf/Topshelf/issues/227

[33mcommit 48c48f29e28c417fe0ff3fce7369ffda52f44105[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Mar 25 17:27:30 2015 +1000

    Update README.md

[33mcommit 74dad408eb9496555a50c656ebab01fe21efd9a5[m
Merge: db68ee71 b34714c8
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Mar 8 11:46:25 2015 +1000

    Merge pull request #366 from chuwik/periodicsink-changes
    
    #357 PeriodicBatchingSink now flushes on unhandled exceptions

[33mcommit 54ed61c29299cef1cc00b61e8d1be0747fa11396[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Mar 7 15:49:01 2015 +1000

    Changelist

[33mcommit cad467cd91fbc7dfee83d74a6658445313a7e629[m
Merge: 98f49f5a db68ee71
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Mar 7 15:48:05 2015 +1000

    Merge branch 'dev'

[33mcommit db68ee71ce3d0a390e2fceddcc2957991c7bab64[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Mar 7 15:34:55 2015 +1000

    Remove references to sink projects from the build script

[33mcommit daecef89d3489edf0701495cfdf15cd88283dd36[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Mar 7 15:19:57 2015 +1000

    Moved the remaining sink projects to separate repositories under https://github.com/serilog (#344)

[33mcommit 95b38953d488ac414810126a1ec8522bf666847a[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Feb 28 22:06:17 2015 +1000

    Actually remove the timing project, this time

[33mcommit aeed240b9d0fdaa1bd8b58057b7b5b5aa7a83aa7[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Feb 28 21:37:55 2015 +1000

    Removed Serilog.Sinks.Elasticsearch (now at https://github.com/serilog/serilog-sinks-elasticsearch) and Serilog.Extras.Timing (now https://githhub.com/serilog-metrics/serilog-metrics).

[33mcommit 0cfd1b680812618c92a9137a635b299958d0497a[m
Merge: 98f49f5a e1379f54
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Feb 28 21:26:16 2015 +1000

    Merge branch 'mookid8000-es-sink-durability-mode' into dev

[33mcommit e1379f548b7f9dcb991fc69b0b5324c7ced93503[m
Merge: 98f49f5a 35384861
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Feb 28 21:25:33 2015 +1000

    Merge branch 'es-sink-durability-mode' of https://github.com/mookid8000/serilog into mookid8000-es-sink-durability-mode
    
    Conflicts:
            Serilog.sln
            src/Serilog.Sinks.ElasticSearch/LoggerConfigurationElasticSearchExtensions.cs
            src/Serilog.Sinks.ElasticSearch/Serilog.Sinks.ElasticSearch.csproj
            src/Serilog.Sinks.ElasticSearch/Sinks/ElasticSearch/ElasticSearchSink.cs
            src/Serilog.Sinks.ElasticSearch/Sinks/ElasticSearch/ElasticsearchSinkOptions.cs

[33mcommit 98f49f5ab3dd23545603281088d90a37d713bb51[m
Merge: 74d283aa 3f76e3ef
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Feb 23 08:39:13 2015 +1000

    Merge branch 'dev'

[33mcommit 3f76e3ef66a9fc5c9970633240f3df996b579b0b[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Feb 23 08:39:03 2015 +1000

    Changelist

[33mcommit 1fc5853954f8569126f3c21d18fe71055d2d9419[m
Merge: 072bd6b0 6d7f52b0
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Sat Feb 21 09:24:43 2015 +1000

    Merge pull request #393 from merbla/feature-remove-splunksink
    
    Removed Splunk sinks ready for move to new repo

[33mcommit 6d7f52b07b37b2b478281c6f7a96ad136d35153a[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Sat Feb 21 09:09:46 2015 +1000

    More solution fun

[33mcommit e601346d06b524c62b0796bfa5fc142f5fa73b2d[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Sat Feb 21 08:54:54 2015 +1000

    Removed Splunk from 2015 solution

[33mcommit 04faf21ead4073e9ada48add935e241361daf8f5[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Sat Feb 21 08:29:34 2015 +1000

    Removed Splunk sinks ready for move to new repo

[33mcommit 35384861bb26488009b884f4a919d50a2970d5b9[m
Author: Mogens Heller Grabe <mookid8000@gmail.com>
Date:   Thu Feb 19 08:44:22 2015 +0100

    added ability to customize the formatter used when converting log events into ElasticSearch documents

[33mcommit cccc726af3a841933954da95d24512a2db3d5077[m
Author: Mogens Heller Grabe <mookid8000@gmail.com>
Date:   Wed Feb 18 11:14:43 2015 +0100

    fixed timeout thingie to work in .NET 4 and .NET 4.5

[33mcommit 87be7b4884bc8ac0e8f97998d2bffc0e77793bd2[m
Author: Mogens Heller Grabe <mookid8000@gmail.com>
Date:   Wed Feb 18 10:49:27 2015 +0100

    removed Console.WriteLine stuff + added file headers

[33mcommit 73f0f7fcfffdc67508269d76943b894fc779f0d2[m
Author: Mogens Heller Grabe <mookid8000@gmail.com>
Date:   Wed Feb 18 09:43:01 2015 +0100

    removed last remains of test project

[33mcommit 0110e150145588cac98a67ca55715df74e53808c[m
Author: Mogens Heller Grabe <mookid8000@gmail.com>
Date:   Tue Feb 17 14:41:46 2015 +0100

    Added durability option to the ElasticSearch sink, similar to how the Seq
    sink achieves reliable log forwarding.

[33mcommit b34714c8b167cb9345b7f6ee9f95c203f1252d9e[m
Author: Enrique <chuwik@gmail.com>
Date:   Tue Feb 17 22:00:14 2015 +0000

    Updating PeriodicBatchingSink-net40 as well

[33mcommit 103ce9c4e6d7ca298f2c751b675f15595b306742[m
Author: Enrique <chuwik@gmail.com>
Date:   Tue Feb 17 21:54:26 2015 +0000

    Fixing event unsubscription

[33mcommit 072bd6b092175ffa559256f1e34acc1a09b3b21b[m
Merge: 1811d310 f1ede16c
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Feb 18 07:33:26 2015 +1000

    Merge pull request #389 from mookid8000/bug-es-uri
    
    Bug es uri

[33mcommit f1ede16ce705b8835e8a04541041587ee8248932[m
Author: Mogens Heller Grabe <mookid8000@gmail.com>
Date:   Tue Feb 17 14:42:08 2015 +0100

    yet another loca host ;)

[33mcommit 9849a38fc6118ed72835fd701d303de703d12b58[m
Author: Mogens Heller Grabe <mookid8000@gmail.com>
Date:   Tue Feb 17 14:36:47 2015 +0100

    made the fact that the es sink options are optional more obvious by making the parameter optional 4 real

[33mcommit a35ea912910beeb2b7374efb4018e550916c6e09[m
Author: Mogens Heller Grabe <mookid8000@gmail.com>
Date:   Tue Feb 17 14:36:05 2015 +0100

    corrected es sink's default URI (spelling error in 'localhost')

[33mcommit 1811d310e157b8f7537eb78f3e4fe8b1e960c2d5[m
Merge: 74d283aa 0a33ca04
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Feb 17 08:27:34 2015 +1000

    Merge pull request #385 from STRUDSO/dev
    
    Added IndexDecider Func to allow for a custom index pr logevent

[33mcommit 3fa70991ffcd9acfe76005e83f738dbb76a56993[m
Author: Enrique <chuwik@gmail.com>
Date:   Mon Feb 16 22:21:53 2015 +0000

    Inlining method

[33mcommit 0a33ca04965aa3565a59b8810a7039359d84eafb[m
Author: Søren Trudsø Mahon <strudsomahon@ebay.com>
Date:   Mon Feb 16 08:05:32 2015 +0100

    IndexDecider: Renamed TestClass

[33mcommit 74d283aabcef21f4947dc0c3bcbc2ee43d1dc0f2[m
Merge: f78a736e 81e44d70
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Feb 16 08:14:02 2015 +1000

    Merge branch 'dev'

[33mcommit f78a736ee86449e598f3b7f56bc907fad5e862c2[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Feb 16 08:13:47 2015 +1000

    Changelist

[33mcommit 81e44d70bc54e477ec7dd0ff1bafc3aac90c5d48[m
Merge: 5ea061b5 30700dff
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Feb 16 07:49:52 2015 +1000

    Merge pull request #387 from CaioProiete/appsettings-env-variables
    
    #386 Expand environment variables on Extras.AppSettings values

[33mcommit 5ea061b52e650d3da473b3fec73c260deddb6fdf[m
Merge: bcb3782c 5af3655b
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Feb 16 07:31:27 2015 +1000

    Merge pull request #388 from ThomasArdal/dev
    
    Newest elmah.io package

[33mcommit 5af3655b165cf1bdc3c90227f38359fc9f3c3dac[m
Author: Thomas Ardal <thomasardal@gmail.com>
Date:   Sun Feb 15 19:54:28 2015 +0100

    Updated Serilog.Sinks.ElmahIO.nuspec to point to the newest stable package of elmah.io.

[33mcommit 30700dffaec84b6aa25073d24cb3ee9bfb4a1ac7[m
Author: Caio Proiete <caio.proiete@gmail.com>
Date:   Sat Feb 14 16:33:07 2015 -0400

    #386 Expand environment variables on Extras.AppSettings values

[33mcommit 6ada7ec6be42332359334b33638c73d13fc160c9[m
Author: Søren Trudsø Mahon <strudsomahon@ebay.com>
Date:   Fri Feb 13 10:14:35 2015 +0100

    Added IndexDecider Func to allow for a custom index pr logevent

[33mcommit bcb3782c618a18dbcf77349f49af95214b102629[m
Merge: 65dc216c 840f4565
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Feb 13 07:26:51 2015 +1000

    Merge pull request #382 from steven-dawkins/dev
    
    Fixes support for using multiple CounterMeasure

[33mcommit 840f45654569bf2eed628ba416b663aac6dff925[m
Author: Steven Dawkins <steven.dawkins@gmail.com>
Date:   Tue Feb 10 13:50:55 2015 +0000

    Test for using multiple counters

[33mcommit 7ddac519d1e98c65b6c461f17c1e287cb8e874d6[m
Author: Steven Dawkins <steven.dawkins@gmail.com>
Date:   Tue Feb 10 13:49:46 2015 +0000

    CounterMeasure value no longer a static variable

[33mcommit 65dc216c1d4b594d2d49a93e34543c4221fc70c0[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Feb 8 11:37:14 2015 +1000

    Version number

[33mcommit d6f5bc76b572bc5e7318193e02fef63b8502d99b[m
Merge: 9532db24 9c899209
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Feb 8 11:36:41 2015 +1000

    Merge branch 'dev'

[33mcommit 9c8992092ea43837e7bab2bcac125618df8d1aca[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Feb 8 11:36:29 2015 +1000

    Changelist

[33mcommit 3f9763e9b38b0b281bee0ac7d940d74ed8919078[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Feb 8 11:35:00 2015 +1000

    Removed the loggly sink, moved to serilog/serilog-sinks-loggly (#344)

[33mcommit eca69fc6655716f7eef40031396c76ea9cd97da8[m
Merge: c5b8b4f5 e924b041
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Feb 8 11:31:46 2015 +1000

    Merge pull request #376 from tetious/feature-fix-loggly-async
    
    Wait for Loggly client to submit the log event.

[33mcommit c5b8b4f51a186eca80d178fe366da9bffb1a1e3d[m
Merge: 09e9ce2c 0063a4a6
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Feb 8 08:14:30 2015 +1000

    Merge pull request #379 from vossad01/dev
    
    Corrects a minor spelling mistake in the description of the MSOwin nuget package

[33mcommit 09e9ce2c6cf103fc4804f939d9508a84df81dd7f[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Feb 8 08:06:06 2015 +1000

    Changelist

[33mcommit 7f8622a16e948e49120a3b651828299906c10445[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Feb 8 08:02:14 2015 +1000

    Removed monotouch/droid projects from the solution; fixes #377

[33mcommit 7119eb6b258daca3cce18829c26849d921942de9[m
Merge: 9532db24 816630a7
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Feb 8 08:01:29 2015 +1000

    Merge branch 'Jaben-Issue#30' into dev

[33mcommit 816630a75d106c58d78704685b97f5bf394c64a5[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Feb 8 07:59:49 2015 +1000

    Tiny whitespace change

[33mcommit 0063a4a6adc3ba7b244f14e75979960fafce15b5[m
Author: Adam Voss <vossad01@gmail.com>
Date:   Sat Feb 7 15:24:40 2015 -0600

    Corrects a minor spelling mistake in the description of the MSOwin nuget package

[33mcommit e924b041eed543abad274174dfd744e6a81c7162[m
Author: Greg Lincoln <greg@greglincoln.com>
Date:   Thu Feb 5 17:34:10 2015 -0500

    Convert LogglySink to PeriodicBatchingSink.

[33mcommit 9597af482300e222edbeb2f37b87ac21833a9201[m
Author: Greg Lincoln <greg@greglincoln.com>
Date:   Thu Feb 5 13:17:22 2015 -0500

    Wait for Loggly client to submit the log event.

[33mcommit 3e4ce29d80b1ae854f6b0e35c3b32bf16b6c90f4[m
Author: Jaben Cargman <jaben.cargman@captiveaire.com>
Date:   Tue Feb 3 08:29:48 2015 -0500

    Didn't have a VS2015 install. Installed and now fixed the compiling issues.

[33mcommit 0b3cee83abfc2f07ec5f7c9d97fe775167419986[m
Author: Jaben <jaben-accounts@tinygecko.com>
Date:   Mon Feb 2 21:49:09 2015 -0500

    Taking a stab at issue #30. The IDestructuringPolicies are now run before IEnumerable cast -- allowing an opportunity for policies to support IEnumerable values. Added IsValueTypeDictionary function to simplify the look of the dictionary conversion code.

[33mcommit 9532db24ebc6e63094f4b0a2b5245ebf7b867a6a[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Feb 1 13:20:12 2015 +1000

    Add _CommonAssemblyInfo.cs_ to the new Azure DocumentDB sink, and fix some capitalisation conventions while there (https://msdn.microsoft.com/en-us/library/vstudio/ms229043%28v=vs.100%29.aspx)
    
    The caps change is a breaking one but since the sink is brand new, better to get it in now.

[33mcommit 1afb9fbb58f8e4df555b759ba2040ef20343d511[m
Merge: 26fb7fe7 aa4be293
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Feb 1 12:26:12 2015 +1000

    Merge branch 'dev'

[33mcommit aa4be293ef293634018da3df4dffa7f1e38a3642[m
Merge: 3cabe57e 6f737de7
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Feb 1 12:25:58 2015 +1000

    Merge branch 'gertjvr-master' into dev

[33mcommit 6f737de73cdc1d6f8f58f5af56297b8a7e9dc331[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Feb 1 12:25:24 2015 +1000

    Use a property rather than public field, and add some docs.

[33mcommit 8ce52643ee38788e75c255602eaf81389a978396[m
Merge: 3cabe57e 2e24f840
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Feb 1 12:16:54 2015 +1000

    Merge branch 'master' of https://github.com/gertjvr/serilog into gertjvr-master
    
    Conflicts:
            Serilog.sln

[33mcommit 3cabe57e3a1fb206a11ebb4e65d57e892aeb5722[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Feb 1 12:06:45 2015 +1000

    Changelist

[33mcommit cd904fe91f6c73a4c780f6ad01d5436f5bf4a2c6[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Feb 1 12:06:36 2015 +1000

    Changelist

[33mcommit 6272791bcac40390f09ff8885057e103e65d2110[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Feb 1 12:03:39 2015 +1000

    Moved the Application Insights sink to https://github.com/serilog/serilog-sinks-applicationinsights
    
    #344

[33mcommit a084331a9850facf6a14f71ba43223700fcd27e1[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Feb 1 11:48:04 2015 +1000

    Moved Serilog.Sinks.MongoDB to its new location at https://github.com/serilog/serilog-sinks-mongodb
    
    #344

[33mcommit 67097de0fe0fe3713589b2c6340223cce9ad8a4b[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Feb 1 11:32:15 2015 +1000

    Changelist

[33mcommit 556034f4a7fc007aeb152d3d588cbc43fafcb9b6[m
Merge: b16ac558 e4065f68
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Feb 1 11:30:08 2015 +1000

    Merge pull request #373 from davidpeden3/dev
    
    upgrade Raygun from 2.0.4 to 4.2.0

[33mcommit e4065f687d5d57670931b7dcbec549f6da5d8a12[m
Author: David Peden <davidpeden3@gmail.com>
Date:   Fri Jan 30 21:10:54 2015 -0600

    upgrade Raygun from 2.0.4 to 4.2.0

[33mcommit b16ac558c7c309fd34948ae7fda0bfebc0718281[m
Merge: d6853912 3d382257
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Jan 29 07:49:37 2015 +1000

    Merge branch 'dev' of https://github.com/serilog/serilog into dev

[33mcommit d6853912e9ba62e1d8cadf8522e02ee1ef4205ab[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Jan 29 07:49:02 2015 +1000

    Mark the updated Mongo sink as -pre so that the `dev` build doesn't break

[33mcommit b371f224c3e9136cf4a57c4e6119501612a346e1[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Jan 29 07:47:29 2015 +1000

    Changelist

[33mcommit 3d3822577aea25b2c770e73a9cfc78ac4ab74eb8[m
Merge: 9b43a72d ea549416
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Jan 29 07:45:50 2015 +1000

    Merge pull request #369 from jensbengtsson/patch-1
    
    [Elasticsearch Sink] Log message should be named "message" instead of "r...

[33mcommit ea549416f517a1c625cb3d0c708e22e7f3a2a4d3[m
Author: Jens Bengtsson <jens.bengtsson@findwise.com>
Date:   Wed Jan 28 12:54:58 2015 +0100

    [Elasticsearch Sink] Log message should be named "message" instead of "renderedMessage"
    
    This change was made in the other elasticsearch json formatter, but not in the .net 4.0 one. So this should be changed here as well.

[33mcommit 3fe70fc00fab1a6ddecacc53dc6c365d2bfc09b2[m
Merge: 9b43a72d d70a1c10
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Jan 28 08:19:07 2015 +1000

    Merge branch 'aaronpowell-dev' into dev

[33mcommit d70a1c102ec140005ecfb65e1c80e8e4dcafe8bd[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Jan 28 07:57:18 2015 +1000

    Changelist

[33mcommit 148b4d518c84e5632e66246c1b796728cd6975eb[m
Merge: 9b43a72d f9c5c714
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Jan 28 07:52:47 2015 +1000

    Merge branch 'dev' of https://github.com/aaronpowell/serilog into aaronpowell-dev

[33mcommit 9b43a72df03ac89b76be72ad757dc6dbadf3071d[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Jan 28 07:46:44 2015 +1000

    Changelist

[33mcommit 9f108e3c654de91947059b2c1ede2f92599acabc[m
Merge: 0ff425bd 7a15584f
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Jan 28 07:43:47 2015 +1000

    Merge branch 'jbattermann-dev' into dev

[33mcommit 7a15584fda615df185ed6005e30457bc502c385e[m
Merge: 0ff425bd 5b0457ba
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Jan 28 07:41:37 2015 +1000

    Merge branch 'dev' of https://github.com/jbattermann/serilog into jbattermann-dev

[33mcommit 7dc125d4c0288facffa7f20847bdb1f5e9cb7d75[m
Author: Enrique <chuwik@gmail.com>
Date:   Mon Jan 26 00:51:58 2015 +0000

    #357 PeriodicBatchingSink now flushes on unhandled exceptions

[33mcommit 0ff425bd2f8eabeee535c2471fba72a2e4ac69e2[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Jan 25 07:41:11 2015 +1000

    Update README.md

[33mcommit 5b0457ba9524182f214043df2c7b00ca588b12d1[m
Author: Jörg Battermann <jb@joergbattermann.com>
Date:   Fri Jan 23 22:26:12 2015 +0100

    Updated Serilog.Sinks.ApplicationInsights
    
    .. to the new Azure Portal based Application insights.

[33mcommit f9c5c7145b73ba18f0d5cd8839c2bd2094262ab8[m
Author: Aaron Powell <me@aaron-powell.com>
Date:   Fri Jan 23 10:26:19 2015 +1100

    Updating the MongoDB sink to work with the MongoDB c# v2 driver preview

[33mcommit 26fb7fe7844120e2804c85771047b2305a09b633[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Jan 23 08:37:43 2015 +1000

    Changelist

[33mcommit 70b44b3336e85c1ffcb83af602ebaf89c90a0a76[m
Merge: 321d0060 4ed7ad0a
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Jan 23 08:36:55 2015 +1000

    Merge branch 'dev'

[33mcommit 4ed7ad0a50b11b8987dcc61707b9b6b5e1ec8a9a[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Jan 23 08:36:31 2015 +1000

    Hopefully the last build script tweak...

[33mcommit a263b4bd88e95df51f1d168f5523f933abcf73f9[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Jan 23 08:26:14 2015 +1000

    Build script update to exclude Mono* sinks from packaging

[33mcommit d18c05f6f3d5d0d3594e3335c14736546d816ef1[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Jan 23 08:12:31 2015 +1000

    Updated NuGet dependencies & NuSpec of Loggly sink to fix #362

[33mcommit 60420825a5eee444f072dca38d0de7b677059715[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Jan 23 08:06:31 2015 +1000

    NuSpec fix so the Azure Document DB sink builds in the new CI server

[33mcommit 11ec3682156977dd802f1551c36c2a4ccc68d285[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Jan 23 08:03:59 2015 +1000

    Remaining tidy-up from #125; use SourceContext as the Android log tag

[33mcommit bb489e638dee97208e3dcdbf6351b18015d05ca1[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Jan 23 07:49:57 2015 +1000

    #125 - exclude the Xamarin-dependent projects from the build

[33mcommit efb8999d3c9d01bbccc75b0e3178d1df52763ac4[m
Merge: 20f52291 8094a77f
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Jan 23 07:48:00 2015 +1000

    Merge branch 'robdmoore-azure-doc-db' into dev

[33mcommit 8094a77f9b0ce6db150b3d8bb76b49dbcd73e510[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Jan 23 07:47:22 2015 +1000

    Merge branch 'azure-doc-db' of https://github.com/robdmoore/serilog into robdmoore-azure-doc-db
    
    Conflicts:
            Serilog.sln
            packages/repositories.config

[33mcommit 20f5229166a3a84acb0a045b3dda2250c35b2dfa[m
Merge: e58ada5d 86bcdaba
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Jan 23 07:34:17 2015 +1000

    Merge branch 'nberardi-master' into dev

[33mcommit 86bcdaba9ecfb2d92d294ebff005261a15421a8c[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Jan 23 07:33:52 2015 +1000

    Merge branch 'master' of https://github.com/nberardi/serilog into nberardi-master
    
    Conflicts:
            Serilog.sln
            src/Serilog/Policies/NullableScalarConversionPolicy.cs
            src/Serilog/Serilog.nuspec

[33mcommit e58ada5d1bd94eafd039d8c122d6f9096cbed215[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Jan 17 22:17:15 2015 +1000

    Link CommonAssemblyInfo.cs into the Azure Event Hubs sink to fix package versioning during build.

[33mcommit 321d00606c9f235913d2210a6462495c1d24ad7f[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Jan 17 20:54:04 2015 +1000

    Changelist

[33mcommit 3df2a114273ef71934fbfcaf0c82c1820a366ee8[m
Merge: 47601610 08020c39
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Jan 17 20:51:44 2015 +1000

    Merge branch 'dev'

[33mcommit 08020c39c1b81b77259eb6ba68b38a554aab88e2[m
Merge: 47601610 56481a4b
Author: Kristian Hellang <kristian@hellang.com>
Date:   Fri Jan 16 09:31:59 2015 +0100

    Merge pull request #354 from nblumhardt/dev
    
    Adds Serilog.Extras.FSharp

[33mcommit 56481a4b9a33c2e7d09fbc946d9a7c2811c3ca09[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Jan 15 17:47:38 2015 +1000

    Fixed the invalid NuSpec for the F# extra

[33mcommit d6fd49323c3c5eb77598ab63f45e3fb597eeeb2c[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Jan 15 08:19:34 2015 +1000

    Build and NuSpec changes to get Serilog.Extras.FSharp packaging

[33mcommit 15afbbb1d2bd2971a15826567a64ad56b4667a1b[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Jan 15 07:28:55 2015 +1000

    Fixed Serilog.Extras.FSharp assembly name

[33mcommit 45a6cc4e2328b4a28ce1efdec178d92308c0b10d[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Jan 15 07:22:14 2015 +1000

    Adds `Destructure.FSharpTypes()` to Serilog configuration
    
    Closes serilog/serilog#352

[33mcommit 47601610d76009afff654c541a5601016bb2a038[m
Merge: 9d81aeda bb74afcf
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Jan 13 20:39:29 2015 +1000

    Merge branch 'dev'

[33mcommit bb74afcf176676d53c92d7e0cb293babdd21221f[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Jan 13 20:39:01 2015 +1000

    Changelist

[33mcommit b53b256705ae76711c4704d74334c5bb1c6de6ce[m
Merge: 9d81aeda aa1e19a0
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Jan 13 10:08:47 2015 +1000

    Merge pull request #351 from BenGale/feature-azureEventHub
    
    Created a sink for Azure Event Hubs.

[33mcommit aa1e19a0714f8347518c4a99f4cca5eecdae00c2[m
Author: Ben Gale <bengale246@gmail.com>
Date:   Sun Jan 11 21:38:52 2015 +0000

    Changes to meet project contribution standards.

[33mcommit 44b9ef0b11d5e41df339f5d38569c2a230389dea[m
Author: Ben Gale <bengale246@gmail.com>
Date:   Fri Jan 9 22:40:00 2015 +0000

    Created a sink for Azure Event Hubs.

[33mcommit 9d81aeda9abb5da9575e5469727911a4c888f6ec[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Jan 7 07:25:45 2015 +1000

    Changelist

[33mcommit 751bc42b56419c563959b11b71fe4af6c661b688[m
Merge: 17387ac2 8921656f
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Jan 7 07:24:19 2015 +1000

    Merge branch 'dev'
    
    Conflicts:
            CHANGES.md

[33mcommit 8921656f90d7aec7bf6b043bfc8a61f73347e0d1[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Jan 7 07:22:37 2015 +1000

    Changelist

[33mcommit f6578baff3f160923d5c8f956425f707bbfcac75[m
Merge: f2f7cbbb 94a50461
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Jan 7 07:19:24 2015 +1000

    Merge pull request #346 from mharen/dev
    
    pass context properties along to nlog

[33mcommit 94a50461a3dd921b7fe17ccbf55364feaaffdb22[m
Author: Michael Haren <mharen@gmail.com>
Date:   Sun Jan 4 17:50:29 2015 -0500

    fixed literal format detection

[33mcommit 736a4c660cd971bc5e8e3fe51f358e7ca6b855e6[m
Author: Michael Haren <mharen@gmail.com>
Date:   Sun Jan 4 17:11:20 2015 -0500

    only specify the literal format for scalar strings

[33mcommit f2f7cbbbd42454a45278efdc85ea0e2955e91215[m
Merge: bdca6cc5 9a077937
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Jan 5 07:25:25 2015 +1000

    Merge branch 'TrevorPilley-master' into dev

[33mcommit 9a077937ba3becd00c01d8c30eca448cf7d5b528[m
Merge: bdca6cc5 d124968b
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Jan 5 07:24:42 2015 +1000

    Merge branch 'master' of https://github.com/TrevorPilley/serilog into TrevorPilley-master

[33mcommit d124968b56752b8db9cd845cc9fbc61a0740b9ee[m
Author: Trevor Pilley <trev@trevorpilley.co.uk>
Date:   Sat Dec 27 13:22:10 2014 +0000

    Updated the nuspec to SignalR 2.1.0

[33mcommit 454cd0b62ad81959bd26178b3322b3b0379ebbd6[m
Author: Trevor Pilley <trev@trevorpilley.co.uk>
Date:   Sat Dec 27 13:19:59 2014 +0000

    Updated to SignalR 2.1.0
    
    There's a breaking change in SignalR 2.1.0 which makes code compiled
    against 2.0.0 unable to be binding redirected - hence the update

[33mcommit dd3e84ab8b06ca0bdc1b74f58c665031481d782a[m
Author: mharen <mharen@gmail.com>
Date:   Wed Dec 24 09:29:02 2014 -0500

    pass context properties along to nlog

[33mcommit 17387ac2cfc01dbf60936003dd766482dba5a29d[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Dec 22 08:17:37 2014 +1000

    Changelist

[33mcommit bdca6cc5445aed0c4260a104bb51fbdfe821b2af[m
Merge: 1e1cc57e 89f3b142
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Dec 22 08:15:58 2014 +1000

    Merge pull request #329 from bragma/dev
    
    Improved Azure Table Storage Sink

[33mcommit 1e1cc57ea7023b006916f95099717ff34ec50878[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Dec 19 08:30:40 2014 +1000

    Changes.md

[33mcommit d56fdda396303ff77f2579ee7fc88eaa9803f425[m
Merge: 5fbcfd1e 9a2040c5
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Dec 19 08:28:39 2014 +1000

    Merge branch 'dev'

[33mcommit 9a2040c5445e45dca026788a29e2642f6778f3a0[m
Merge: 5b88267c 88043e69
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Dec 19 08:04:38 2014 +1000

    Merge pull request #341 from paulblamire/feature-ApplicationLifeCycleModule-RequestLoggingLevel
    
    Feature application life cycle module request logging level

[33mcommit 5fbcfd1e4f0850ca262ae8b3c3e27ac12497991b[m
Merge: 9155620c 732c1815
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Thu Dec 18 19:26:01 2014 +1100

    Merge pull request #342 from richiej84/Custom_PropoertyEnricher_Support
    
    Change LogContext.PushProperties to accept ILogEventEnrichers rather tha...

[33mcommit 732c1815448cf1bbcc112487a1aabe8e842f387d[m
Author: richiej84 <richiej84@gmail.com>
Date:   Wed Aug 13 23:28:00 2014 -0600

    Change LogContext.PushProperties to accept ILogEventEnrichers rather than just PropertyEnrichers
    
    (cherry picked from commit 1c41ab8fc8dcf0e4526365550ae9d8b0a33890a3)

[33mcommit 9155620ccbd98f4c5bfd70b227c21f2466972dd9[m
Author: Michiel van Oudheusden <m.vanoudheusden@gmail.com>
Date:   Tue Dec 16 23:00:17 2014 +0100

    Changed link to documentation.
    
    Points the documentation link to the serilog/serilog/wiki page instead of the https://github.com/nblumhardt/serilog/wiki site where there is no content.

[33mcommit 88043e691079cf4824b518f51ee5d332e5c4f92b[m
Author: Paul Blamire <paul_blamire@yahoo.com>
Date:   Tue Dec 16 19:51:23 2014 +0000

    Rename LoggingLevel to RequestLoggingLevel
    
    Took on board comment from pull request #330 to change naming. Also
    branched to so I can submit a new pull request targeting develop rather
    than master

[33mcommit 5b88267c078811d51db383b7f5220b17bfbc35db[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Dec 15 08:03:05 2014 +1000

    Changelist

[33mcommit e20a8833475ef1f045c2f9a9bbcefcae19ccef6c[m
Merge: 600c0e79 335699f4
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Dec 15 07:29:29 2014 +1000

    Merge branch 'object-master' into dev

[33mcommit 335699f4791f73fcb1208c8f237635406a6178fd[m
Merge: 600c0e79 e3a18ab7
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Dec 15 07:29:08 2014 +1000

    Merge branch 'master' of https://github.com/object/serilog into object-master

[33mcommit e3a18ab752db60877c1733ac86a1c512136c9453[m
Author: Vagif Abilov <vagif.abilov@gmail.com>
Date:   Fri Dec 12 15:57:29 2014 +0100

    Added optional Encoding parameter to RollingFileSink and FileSink constructors.

[33mcommit 89f3b142d3bb087a27bbb216b421b5d993f6c8cc[m
Author: Marco Braga <marco.braga@gmail.com>
Date:   Fri Dec 12 00:31:50 2014 +0100

    Aggregate properties exceeding max number to a single property

[33mcommit 600c0e79c2ba31cbe2d06f8a8b7482bf81de96cd[m
Merge: 06ba9ca5 59acfa3e
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Dec 12 06:47:58 2014 +1000

    Merge pull request #336 from merbla/splunkstream
    
    Changed to use stream for HTTP in Splunk (#215)

[33mcommit 59acfa3e7996f7368583f19d9fe324ffdbf61cc2[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Thu Dec 11 12:50:13 2014 +1000

    Changed to use stream for HTTP in Splunk

[33mcommit 06ba9ca5e388a0cbd0cf739374efdda1b130f272[m
Merge: ae15d31a 89004758
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Thu Dec 11 11:31:59 2014 +1000

    Merge pull request #335 from nblumhardt/dev
    
    First checklist item from #125 - remove `dynamic`

[33mcommit 8900475878ca5e4d6b84310a0b56ca21a3c6080d[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Dec 10 20:46:56 2014 +1000

    First checklist item from #125 - remove `dynamic`
    
    Nullable type destructuring can be achieved without using `dynamic` - this change makes it possible to destructure `Nullable<T>` as scalar values on iOS.

[33mcommit 24e5079216300f6673d45123d2a304ec2e6a9c80[m
Author: Paul Blamire <paul_blamire@yahoo.com>
Date:   Tue Dec 9 18:19:44 2014 +0000

    Allow logging of HTTP requests at a level other than Information

[33mcommit dc1a509b159d239c7d9ca25217083c929c6cb530[m
Merge: ae15d31a f1b6d995
Author: Marco Braga <marco.braga@gmail.com>
Date:   Tue Dec 9 14:27:23 2014 +0100

    Merge branch 'azure-with-properties' into dev
    
    Conflicts:
            Serilog.sln

[33mcommit 2d7213f0e856ac2c1f1be8e87c981a5bc22f0461[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Dec 8 17:25:49 2014 +1000

    Remove 4.0 PDBs from ES sink package, breaks SymbolSource publishing (reverts #323)

[33mcommit d7e4e38b59ecd10e4d230daccb097b1318abdd38[m
Merge: 0439e738 ae15d31a
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Dec 8 08:37:07 2014 +1000

    Merge branch 'dev'

[33mcommit 0439e73860941c903088c971980f20d31997f717[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Dec 8 08:37:01 2014 +1000

    Changelist

[33mcommit ae15d31a466899c16e66ac266d95b1b02af0f346[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Dec 8 08:22:00 2014 +1000

    Fixes #321 - `renderedMessage` vs. `message` property name regression

[33mcommit e3447ad93362d2a09b969dbebe2174d5d2966298[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Dec 8 08:19:36 2014 +1000

    Fixes #323 - include PDBs in ES sink

[33mcommit d4e33acc81d08212d6046ade9b2f6db13cd1be87[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Dec 8 08:16:17 2014 +1000

    Fixes #234 - ElasticsearchSink needs to calculate index names for Logstash using UTC

[33mcommit 8843b61bc7a354c133bff7cad952c6a7bd86cf41[m
Merge: 1486ed1d 8399da67
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Dec 8 08:08:57 2014 +1000

    Merge branch 'master' of https://github.com/mookid8000/serilog into mookid8000-master

[33mcommit 1486ed1d90c52ea73bf3cc6b05ca36dd3de152c6[m
Merge: 2d461ad9 463a2ef4
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Dec 8 08:06:09 2014 +1000

    Merge branch 'StephenHynes7-fix-logentries-ssl' into dev

[33mcommit 463a2ef4449f877e26a29138ba3981c37d65c47d[m
Merge: 2d461ad9 cf4806e8
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Dec 8 08:05:53 2014 +1000

    Merge branch 'fix-logentries-ssl' of https://github.com/StephenHynes7/serilog into StephenHynes7-fix-logentries-ssl

[33mcommit 2d461ad98f64b7b7161b3c698cae7e4dcfb70b84[m
Merge: 6651137f 58dc7e11
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Dec 8 08:03:36 2014 +1000

    Merge pull request #327 from khellang/dev
    
    Added EditorBrowsable attributes to Obsolete methods

[33mcommit 8399da67dd1f974f049264d475a68d3cd2f5d554[m
Author: Mogens Heller Grabe <mookid8000@gmail.com>
Date:   Sun Dec 7 21:48:44 2014 +0100

    added configuration overload that makes it more explicit that multiple email recipients can be specified and added appropriate comment to email connection info

[33mcommit 58dc7e11625f58db9879345620d4e80da3c394f0[m
Author: Kristian Hellang <kristian@hellang.com>
Date:   Sun Dec 7 16:39:24 2014 +0100

    Added EditorBrowsable attributes to Obsolete methods

[33mcommit f1b6d995cdaf46518b04b056256ba95cdbbe7364[m
Author: Marco Braga <marco.braga@gmail.com>
Date:   Fri Dec 5 10:40:24 2014 +0100

    Updated tests with startup fixture

[33mcommit 7096b284f60339e109c3a03e8ea8c361f648f6d7[m
Author: Marco Braga <marco.braga@gmail.com>
Date:   Fri Dec 5 10:03:32 2014 +0100

    Removed appveyor script, start/stop emulator directly from test startupfixture for a more generic solution.

[33mcommit fde6fbeaa258c4086e6acdafc1be2e3a829becf6[m
Author: Marco Braga <marco.braga@gmail.com>
Date:   Fri Dec 5 09:00:14 2014 +0100

    Updated appveyor script

[33mcommit 63c0842a8fa952d3359561b02a13c4a2f080b59b[m
Author: Marco Braga <marco.braga@gmail.com>
Date:   Fri Dec 5 08:55:31 2014 +0100

    Updated appveyor script

[33mcommit a139a35f17b0670108c4566d8388e77707e1bdf5[m
Author: Marco Braga <marco.braga@gmail.com>
Date:   Fri Dec 5 08:52:59 2014 +0100

    Updated appveyor script

[33mcommit 9e91cb69e478e0bb2b8dc6ea5939d2000c2aed60[m
Author: Marco Braga <marco.braga@gmail.com>
Date:   Fri Dec 5 08:50:13 2014 +0100

    Updated appveyor script

[33mcommit 0fe7cde04bb485f927e11ff989652be9b92ebbf0[m
Author: Marco Braga <marco.braga@gmail.com>
Date:   Fri Dec 5 08:44:53 2014 +0100

    Updated appveyor script

[33mcommit f98a5064015a581466ff464b68cd0fa30093f347[m
Author: Marco Braga <marco.braga@gmail.com>
Date:   Fri Dec 5 08:36:42 2014 +0100

    Updated appveyor script

[33mcommit 7e63c8735a096d112d8b6fbddea49d58389a7513[m
Author: Marco Braga <marco.braga@gmail.com>
Date:   Fri Dec 5 08:34:43 2014 +0100

    Updated appveyor script

[33mcommit 39807ad1aa5efbf051a8f91e17fe8c4b0ba46532[m
Author: Marco Braga <marco.braga@gmail.com>
Date:   Fri Dec 5 08:33:01 2014 +0100

    Fixed script

[33mcommit 49e528180e57f5d4df68832bd36f79151f77c6dd[m
Author: Marco Braga <marco.braga@gmail.com>
Date:   Fri Dec 5 08:26:58 2014 +0100

    Added appveyor config for starting Azure Storage emulator before testing

[33mcommit cf4806e809fe0c2b3a05d358044ac61ea77fed6d[m
Author: Stephen Hynes <stephen.hynes@logentries.com>
Date:   Thu Dec 4 09:46:13 2014 +0000

    Remove SSL cert pinning
    
    Remove cert pinning.

[33mcommit f8d18beb4badcb26d22d44870408d37e4268e01b[m
Author: Marco Braga <marco.braga@gmail.com>
Date:   Thu Dec 4 09:12:51 2014 +0100

    Added some comments to batch sink

[33mcommit de2afe46f6c5d19992117d58dd35a0abb3596505[m
Author: Marco Braga <marco.braga@gmail.com>
Date:   Thu Dec 4 01:33:12 2014 +0100

    Added batched sink

[33mcommit 7edfd1d30059aaabf47cbff2137f8e27a20dd74d[m
Author: Marco Braga <marco.braga@gmail.com>
Date:   Tue Dec 2 18:27:15 2014 +0100

    Tests for sequences

[33mcommit 3c0dfc8caafc87e3b7cff572e5b7ae47460af320[m
Author: Marco Braga <marco.braga@gmail.com>
Date:   Tue Dec 2 10:48:55 2014 +0100

    Added tests for entity factory

[33mcommit 9e0ded25ccc5ed856ee106cbc324d8dfe9b1952a[m
Author: Marco Braga <marco.braga@gmail.com>
Date:   Tue Dec 2 09:55:57 2014 +0100

    Support scalar types natively and complex types through ToString()

[33mcommit 26a69d04e46b841060e0e1fcfeb77797b757f874[m
Author: Marco Braga <marco.braga@gmail.com>
Date:   Tue Dec 2 01:06:27 2014 +0100

    Support for additional row key postfix. Use GUID for unique rows.

[33mcommit 4fb0af56fad5d2326ffe3941430a18b04156cf41[m
Author: Marco Braga <marco.braga@gmail.com>
Date:   Mon Dec 1 18:18:26 2014 +0100

    Default table name set to same used in previous sink. Fixed duplicated configuration settings.

[33mcommit c30222945b755723ee775e16b0688c9f4bcad7ce[m
Author: Marco Braga <marco.braga@gmail.com>
Date:   Mon Dec 1 17:54:35 2014 +0100

    Added basic test. Added sink configuration. Added basic sink with properties.

[33mcommit 53fbaa2d10d7c88cfa59f80ecc640c471567eb6f[m
Author: Marco Braga <marco.braga@gmail.com>
Date:   Mon Dec 1 12:56:50 2014 +0100

    Added check for properties in test

[33mcommit f08e7cb4e401640263ea7ecb6dbf552077e2f50e[m
Author: Marco Braga <marco.braga@gmail.com>
Date:   Mon Dec 1 11:10:08 2014 +0100

    Updated RavenDB to use latest Azure Table Storage SDK

[33mcommit e8277d6e1abe8f32e9bc3d301a0238d2b475a5d8[m
Author: Marco Braga <marco.braga@gmail.com>
Date:   Mon Dec 1 11:07:54 2014 +0100

    Added tests for Azure Table Storage.
    Initial changes for supporting first class properties as columns. Added configuration and new sink in existing project.

[33mcommit c1ae9ee0fb29a7e31141e8c907887d6fa7b81947[m
Author: Mogens Heller Grabe <mookid8000@gmail.com>
Date:   Mon Dec 1 10:10:50 2014 +0100

    changed EmailSink to accept a comma-and-or-semicolon separated list of recipients

[33mcommit 2e24f840aca3a9071c2ccd1ed333bf5792204c0f[m
Author: Gert Jansen van Rensburg <gertjvr@gmail.com>
Date:   Thu Nov 27 11:44:44 2014 +1000

    Added LogContext.PermitCrossAppDomainCalls.

[33mcommit e6076f361fb4647dae2c5562d049a65fe70d00c4[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Nov 27 08:30:57 2014 +1000

    Changelist

[33mcommit 952332cdfa4c0e08139a967b41e70de8a83a7993[m
Merge: 81521330 6651137f
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Nov 27 08:29:44 2014 +1000

    Merge branch 'dev'

[33mcommit 6651137f78b407a5d7e14686b41876e13470610f[m
Merge: 37849a48 9a4e30e4
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Nov 27 08:18:46 2014 +1000

    Merge branch 'Mpdreamz-fix/elasticsearch-sink' into dev

[33mcommit 9a4e30e44a61ae8de5d510d9d29394ac7d4f1fa1[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Nov 27 08:18:14 2014 +1000

    Renamed `WriteObjectValue` to `WriteLiteralValue` just to keep some more method name options open in the future

[33mcommit e68b96457a7e1da6267448c00bd19f7917a0b6bc[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Nov 27 08:06:14 2014 +1000

    Fixed build - JsonFormatter differs between .NET 4 and 4.5.

[33mcommit 009183f09502ff4307aff4bf06267e823af89101[m
Merge: 81521330 74b817a9
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Nov 27 07:40:10 2014 +1000

    Merge branch 'fix/elasticsearch-sink' of https://github.com/Mpdreamz/serilog into Mpdreamz-fix/elasticsearch-sink

[33mcommit 5f17a88531c88c4baa6c9883629668f422f940e5[m
Author: Gert Jansen van Rensburg <gertjvr@gmail.com>
Date:   Wed Nov 26 20:58:43 2014 +1000

    think I missed something.

[33mcommit d806d3be24cbe44fd6ff233a8f19299a5d351d72[m
Author: Gert Jansen van Rensburg <gertjvr@gmail.com>
Date:   Wed Nov 26 16:42:28 2014 +1000

    moved serilog.mstest to test folder

[33mcommit 1f74e7ad858c0d856c2b7c83f7d7b9deaf1d22ab[m
Author: Gert Jansen van Rensburg <gertjvr@gmail.com>
Date:   Wed Nov 26 16:33:02 2014 +1000

    Fix LogContext to function for cross-domain calls

[33mcommit 81521330f69acfdeb5d4ae3adcbce0390dfe4957[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Nov 25 08:16:59 2014 +1000

    Attempting a build fix - having some trouble building 4.0 locally after installing VS2015 preview so unable to verify

[33mcommit 8a600ebd12e965952181eef1e720559fd1d78d6b[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Nov 25 07:43:05 2014 +1000

    Changelist

[33mcommit 9d63a2648d05c8f482ae6601d7cb012366e09b85[m
Merge: 90e5d4cf 37849a48
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Nov 25 07:42:08 2014 +1000

    Merge branch 'dev'

[33mcommit 37849a488e441169194df9f8feff4372a15ca726[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Nov 25 07:40:23 2014 +1000

    Fixes #302 - PeriodicBatchingSink batching improvement

[33mcommit 0626ae3743e07752a8f35b9293d6376e9c47d33d[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Nov 25 07:31:03 2014 +1000

    Fixes #304 - suppress IO exceptions when calling WriteTo.File().

[33mcommit 74b817a92f6322c0f387512ff1da4d1631177a4f[m
Author: Martijn Laarman <mpdreamz@gmail.com>
Date:   Mon Nov 24 19:43:11 2014 +0100

    fixed net40 build again

[33mcommit 3170c08c2328d4a7faad774c49b35c2370296c1a[m
Author: Martijn Laarman <mpdreamz@gmail.com>
Date:   Mon Nov 24 18:33:18 2014 +0100

    Added the option to inline fields (write them at the root of the document) as per #234

[33mcommit 45d91ba2409add9a4073ffcb79b69705d9e9d301[m
Author: Martijn Laarman <mpdreamz@gmail.com>
Date:   Mon Nov 24 17:14:46 2014 +0100

    Cleaned up tests, included the ability to override the typename as per #284, Cleaned up classes and changed ElasticSearch to Elasticsearch where possible, Obseleted methods that do not take ElasticsearchSinkOptions

[33mcommit 4ce3112d149b6ce18fc0045f04bb8792049747b1[m
Author: Martijn Laarman <mpdreamz@gmail.com>
Date:   Sun Nov 16 12:20:22 2014 +0100

    This PR brings the default behaviour of the elasticsearch sink in sync with serilog's conservative choice to ToString() unknown objects instead of trying to serialize them.
    
    * Sink now uses a subclass of Json/JsonFormatter.cs, instead of forcing the data into our own LogEvent and relying on ElasticSearchPropertyFormatter to flatten serilogs data structure. We now hook into serilogs preferred serialization routine.
    * Had to add some hooks so that the subclass could override property names.
    * If you provide the elasticsearch sink with an IElasticsearchSerializer instead of ToString()-ing unknown objects the sink will serialize them as json objects allowing events to be filtered/aggregated more granular. For now this only applies to exceptions since objects are flattened to serilog data structures before they are sent to sinks.
    * This allows you to optionally take a dependency on `Elasticsearch.Net.JsonNET`.
    * Added unit tests for the elasticsearch sink.
    * Added an .editorconfig file to force files to use an indentation of 4 spaces
    * The events were always written to an index formatted DateTime.UtcNow, but the event timestamp should be leading what index the event has to go into.

[33mcommit 90e5d4cf04fb4c8a6c378dfa7f018d6def9eb617[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Nov 24 22:37:04 2014 +1000

    Fixes #303 - make sure assembly file versions are unique

[33mcommit b371a23907ae490c77eb8f981de079e744feef4e[m
Merge: 044c7be0 d8ca4cf0
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Nov 24 08:18:15 2014 +1000

    Merge branch 'dev'

[33mcommit d8ca4cf0ee4a6d98dd425925fa0c98daf09dce20[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Nov 24 08:18:00 2014 +1000

    Changelist

[33mcommit b5847d94e89c1e9c4ae7d29b75b3eeba3e28c600[m
Merge: 559da766 0e60b5f2
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Nov 24 08:16:05 2014 +1000

    Merge branch 'merbla-http' into dev
    
    Conflicts:
            CHANGES.md

[33mcommit 0e60b5f2d8c7d36c54df774f0766af5e12bad461[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Nov 24 08:15:12 2014 +1000

    Changelist

[33mcommit 559da7661c1878632fc33fa440893e0674b5a6e4[m
Merge: 4e5b6c5b 8969ea3d
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Nov 24 07:52:59 2014 +1000

    Merge branch 'kanchanm-master' into dev

[33mcommit 8969ea3dba45b3e0cb908f89e460fc864cc889e2[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Nov 24 07:52:45 2014 +1000

    Removed the "Demo" project, since SmokeTest has this covered.

[33mcommit 86e709ca49faa1d2400c59967cc53718dc2b424c[m
Merge: 4e5b6c5b 1e62438e
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Nov 24 07:48:40 2014 +1000

    Merge branch 'master' of https://github.com/kanchanm/serilog into kanchanm-master
    
    Conflicts:
            src/Serilog.FullNetFx/Properties/AssemblyInfo.cs

[33mcommit 30bb50be2e8c5cf4910cf5102495ab13ff45c323[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Sat Nov 22 09:21:54 2014 +1000

    Template change

[33mcommit 097628823c652a3d94aa4300cb3e253fa32b053e[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Sat Nov 22 09:08:19 2014 +1000

    Use LogRequest and use a template on error

[33mcommit 044c7be0bcfcbc1de3c8e713af91ec2b3a619fa1[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Nov 22 06:47:02 2014 +1000

    Build the Splunk package from its NUSPEC rather than the CSPROJ

[33mcommit 44a5c442315391ab8a305d04cdcd55892b51bf93[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Nov 21 19:27:05 2014 +1000

    Changelist

[33mcommit 41791e1d472283de0fc9c39e944900aebcc738f6[m
Merge: cf3ac330 b8765d8a
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Nov 21 19:26:11 2014 +1000

    Merge branch 'johnkattenhorn-master'

[33mcommit b8765d8a47b383f81771cb4bc27c4f5d4509489f[m
Merge: cf3ac330 10a62c15
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Nov 21 19:20:02 2014 +1000

    Merge branch 'master' of https://github.com/johnkattenhorn/serilog into johnkattenhorn-master

[33mcommit cf3ac330ac30c9ccdde470959c68f4fc0b12ea29[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Nov 21 19:15:30 2014 +1000

    Changelist

[33mcommit 90ed101e3aaf1f6cca76fbf019fd02b27e826bb0[m
Merge: 62dd9e8d 4e5b6c5b
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Nov 21 19:13:18 2014 +1000

    Merge branch 'dev'

[33mcommit 4e5b6c5b60a6f371c4d556bfb7c39e928f8475b3[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Nov 21 19:05:06 2014 +1000

    Revert the two more complex packages to build from NUSPEC rather than from CSPROJ

[33mcommit 10a62c150604673a0d385b8396b99862434b1a19[m
Author: johnkattenhorn <john.kattenhorn@applicita.com>
Date:   Thu Nov 20 07:26:46 2014 +0000

    UPDATED: loggly-csharp packages to the latest versions.

[33mcommit 62dd9e8d8c341248913b3b4330a10aee1b874c5d[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Nov 18 11:43:26 2014 +1000

    Disable generation of symbol packages, since AppVeyor's setting to disable publishing doesn't seem to work

[33mcommit 06c965c29d3a1853d2e2664a9da5af3ba35d13e0[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Nov 18 09:58:25 2014 +1000

    Attempt to fix some broken packages

[33mcommit b16f50ed37ecc90ecaa53c3cc9abf058873e5dd5[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Nov 18 08:57:19 2014 +1000

    Remove duplicated files from NUSPECs, and exclude -net40/FullNetFx projects from the packaging step

[33mcommit 346dd0af31a228dfc605630c7ea6ec27b537e92b[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Nov 18 08:23:57 2014 +1000

    Pack NuGet packages during build

[33mcommit 3ffb53587f905d6e201914c1d4a9fbb8f587603a[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Nov 18 08:23:31 2014 +1000

    Internalize XSocketsPropertyFormatter to get rid of a build warning and reduce surface area to port once we have a more general customizable formatting approach

[33mcommit 501e637a1ed1b430b7808d89ad2e640c789bbb2e[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Nov 18 07:53:03 2014 +1000

    First part of #287 - builds script (still needs to pack NUPKGs)

[33mcommit a7e97b6fa4f45890f01843e9ac43c34a7bdd197b[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Nov 16 14:35:01 2014 +1000

    Changlist

[33mcommit 2cfd69eebb0e231fbf3c9024354ac0e37ea0c4f9[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Nov 16 14:07:18 2014 +1000

    Use 1.0.0 as the default informational version so it's easy to spot when it might have been un-patched during a build

[33mcommit 4a2debc043bf5a1eeaa7acf998602886cb55bfaa[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Nov 16 13:31:47 2014 +1000

    Patch up the .NET 4.0 build

[33mcommit ce417bd83b751d54d72db955a899469279f11e1b[m
Merge: 980de25b 04b0aeb5
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Nov 16 11:28:29 2014 +1000

    Merge pull request #264 from khellang/nuspec
    
    Added missing versions to nuspec dependencies

[33mcommit 980de25bfa82619ddf77280741b84fdb7eac2fce[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Nov 16 11:04:42 2014 +1000

    Fixes #227 - reduce the over-generous depth limit and provide `Destructure.ToMaximumDepth(x)` to increase if needed.

[33mcommit b52e6a18a7579b37e64382b499327ab1d2daca05[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Nov 16 10:38:31 2014 +1000

    Fixes #245 - XML escaping in MSSQL sink.

[33mcommit 8e8ecb32a194ac5360502ab467ada5a683ce35ba[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Nov 16 10:13:48 2014 +1000

    Fixes #272 - allow underscores in property names.

[33mcommit 1e62438e805d4c86a530a36353d65c164be32207[m
Author: kanchanm <kanchanm@microsoft.com>
Date:   Tue Oct 28 19:14:28 2014 -0700

    This review includes - source code changes to port Serilog to support
    ASP.NET vNext (includes support for console, file, system.diagnostics to
    start with), smoke test updates. This also includes a temporary Demo
    project to show all the above features working on vNext. The
    Serilog.vs2015 solution is created SxS which includes the new projects as
    well so that people can still open the original solution without issues on
    VS 2013.

[33mcommit 04b0aeb5e6931493c5d4b265ad9b37acad48a1d2[m
Author: Kristian Hellang <kristian@hellang.com>
Date:   Wed Nov 12 02:41:33 2014 +0100

    Added *.nupkg to .gitignore

[33mcommit b3f8b9e69dc26580199bd1d8a5033978eda555a1[m
Author: Kristian Hellang <kristian@hellang.com>
Date:   Wed Nov 12 02:11:55 2014 +0100

    Added $ replacement token to .nuspec

[33mcommit 2f927e5e840bca43e22dac58940c08da35303bb3[m
Author: Kristian Hellang <kristian@hellang.com>
Date:   Wed Nov 12 02:01:58 2014 +0100

    Added CommonAssemblyInfo.cs

[33mcommit 376dc3789b50e3ae72bb12570492b3ffbc99c894[m
Author: Kristian Hellang <kristian@hellang.com>
Date:   Tue Oct 28 00:06:25 2014 +0100

    Added missing versions to nuspec dependencies

[33mcommit 8d1f18872c39f0311829efbcba9f3a9044d37868[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Nov 4 13:27:44 2014 +1000

    Remove packed file.

[33mcommit e268e10a17b381b1a6cc83718a07d42ab415f526[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Nov 4 13:26:18 2014 +1000

    Placate nuget.exe pack - don't try using dependency groups with simple CSPROJ-directed packing.

[33mcommit a0fb9e0320ee8a64597953576c7ee4349310ab9b[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Nov 4 12:44:24 2014 +1000

    One more NuSpec fix

[33mcommit eceb8bdcae76853c518da1b5780f4c781859004d[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Nov 4 12:26:27 2014 +1000

    Fixed a NuSpec file path

[33mcommit 2facab62692ef5cac7abb592dd718a27965a1562[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Nov 4 12:08:22 2014 +1000

    Make NuSpec file inclusion paths project-relative to fit AppVeyor CI convention

[33mcommit e7cad908b5db24382aeec2157f67585223161b6f[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Nov 4 11:48:43 2014 +1000

    Generate XML documentation in release configuration for XSockets sink.

[33mcommit 18c1ffeebf47f06c1b29561f6e3cbd2d54bb9773[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Nov 3 08:25:02 2014 +1000

    Fixed changelist build numbering

[33mcommit f1e2b8fd91d3ef0470de53bb505b46185f227934[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Nov 3 08:24:11 2014 +1000

    Additional changelist item

[33mcommit 3da338e4958e52adcd0aaff3a8acf02cbf3f9f80[m
Merge: b7aba36a 24948e00
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Nov 3 08:21:07 2014 +1000

    Merge branch 'master' of https://github.com/serilog/serilog

[33mcommit b7aba36abcc5031851655bdaa2eb259623e4a34c[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Nov 3 08:19:36 2014 +1000

    Changelist

[33mcommit 24948e0077aac45e505cb6d08ef8d4f27e13e51c[m
Merge: f0aa8c16 aef4b09e
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Nov 3 08:14:59 2014 +1000

    Merge pull request #259 from neutmute/nm-loggly
    
    loggly update to 3.5.1

[33mcommit f0aa8c16e7b7c5816e0a327ca78e481b3a9e3e0f[m
Merge: 025c6341 46727278
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Nov 3 07:39:57 2014 +1000

    Merge pull request #257 from XSockets/master
    
    XSockets.NET Sink for Serilog

[33mcommit 46727278e3be8036cc4e3e8356977b583eb17c03[m
Author: XSockets (@ulfbjo) <codeplanner@gmail.com>
Date:   Wed Oct 29 06:43:48 2014 +0100

    Namespace fix

[33mcommit 025c63413d92e68c04d7d800d81f889e764e8a3d[m
Merge: 815ca294 e8f2b110
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Oct 29 07:11:54 2014 +1000

    Merge pull request #246 from prasannavl/master
    
    Update PCL with Profile259

[33mcommit d933f570841677cd507b409ce350c4af2539bd6d[m
Author: XSockets (@ulfbjo) <codeplanner@gmail.com>
Date:   Tue Oct 28 13:10:23 2014 +0100

    Set batch config to N Blumhardt recomendation

[33mcommit 815ca2942948ef98107f411cdecaa629b2a3374d[m
Author: Kristian Hellang <kristian@hellang.com>
Date:   Tue Oct 28 01:44:44 2014 +0100

    Added CI build badge

[33mcommit 8d3532d92ad0a9f17c1c59ec3fff86ab138bbe6e[m
Author: XSockets (@ulfbjo) <codeplanner@gmail.com>
Date:   Mon Oct 27 07:13:30 2014 +0100

    PR fixes
    
    -Root namespace set to Serilog
    -Properties .cs file now minimal

[33mcommit aef4b09e7f4d54733ebe8b2d1ae6bccace764db7[m
Author: neutmute <neutmute.email>
Date:   Sun Oct 26 17:14:17 2014 +1100

    package updates

[33mcommit da80a567a518f0224366baa24bda7e0a71d93241[m
Author: XSockets (@ulfbjo) <codeplanner@gmail.com>
Date:   Sat Oct 25 14:42:25 2014 +0200

    Removed path to old location for XS sink

[33mcommit 7cfc69fad9eb193bd6081d1b4dfef6f5e9ec8b3e[m
Author: XSockets (@ulfbjo) <codeplanner@gmail.com>
Date:   Sat Oct 25 14:39:47 2014 +0200

    Moved XSockets Sink to correct location

[33mcommit 070d485536caa47d348e75c89dc8de9fbb918a62[m
Author: neutmute <neutmute.email>
Date:   Sat Oct 25 23:34:27 2014 +1100

    update to loggly 3.5.0

[33mcommit 7916134930a87b5bf5a058705ce2a0a14ad7a873[m
Author: XSockets (@ulfbjo) <codeplanner@gmail.com>
Date:   Sat Oct 25 14:28:21 2014 +0200

    Fixed nuspec

[33mcommit 3eb212e5f90c4ba3970f39787ae26b5e4d0cd886[m
Author: XSockets (@ulfbjo) <codeplanner@gmail.com>
Date:   Sat Oct 25 14:24:11 2014 +0200

    Added XSockets Sink

[33mcommit a4ca61bc719183a2151103449518f4c97978dba9[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Oct 24 07:42:31 2014 +1000

    Changelist

[33mcommit da690f8254523fffe49b30b2033c89b37ff91011[m
Merge: 48544170 64fb6116
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Oct 24 07:40:49 2014 +1000

    Merge pull request #250 from IanMercer/mongoDBSink
    
    Allow the MongoDB sink to be configured with an existing MongoDatabase instance

[33mcommit 48544170171faa30019b8a59ea687f87df1e3540[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Oct 24 07:34:49 2014 +1000

    Changelist

[33mcommit bd78cfa4789c3dae902f40e7fed05b31032dbdc1[m
Merge: f5498c97 90ece3a3
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Oct 24 07:33:47 2014 +1000

    Merge pull request #253 from khellang/logmethodattribute
    
    Add LoggerMethodAttribute to identify methods that should use message template formatting

[33mcommit f5498c979ffebdcd7c743d847c99ac34dbffdc4f[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Oct 24 07:11:31 2014 +1000

    Fix ES .NET 4.0 build

[33mcommit 9afe9e7ae7ef2a8c1000bdc4dc2c5173afdc3112[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Oct 24 07:03:35 2014 +1000

    Changelist

[33mcommit 90ece3a304ece925f584783d42c373611b2bfa5b[m
Author: Kristian Hellang <kristian@hellang.com>
Date:   Thu Oct 23 23:03:20 2014 +0200

    Renamed attribute, moved it to Core namespace

[33mcommit 8d54ee8c174b415311d954da31ddd75ecbb1725d[m
Author: Kristian Hellang <kristian@hellang.com>
Date:   Thu Oct 23 21:08:08 2014 +0200

    Added attribute to net40 project

[33mcommit 40c9ab5fb98cf210cd2cef26dc9217248a14b3fd[m
Author: Kristian Hellang <kristian@hellang.com>
Date:   Wed Oct 22 19:48:13 2014 +0200

    Added *.GhostDoc.xml to .gitignore

[33mcommit bdddeee9814dd900c42862e036552d73938fe10d[m
Author: Kristian Hellang <kristian@hellang.com>
Date:   Wed Oct 22 19:47:57 2014 +0200

    Added LoggerMethodAttribute

[33mcommit 69b27f8d613838d2fbf1b7376ec31eba108046d4[m
Merge: 7b8ce343 93d2fec3
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Oct 24 06:59:38 2014 +1000

    Merge pull request #249 from khellang/property-tag-indexes
    
    [WIP] Added StartIndex and EndIndex to MessageTemplateToken

[33mcommit 7b8ce34355a072e916d0e1019a5ef9eef0e33e52[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Oct 24 06:44:24 2014 +1000

    Changelist

[33mcommit 09becfcbeb70554a9692937f67b3631abb138bc5[m
Author: Kristian Hellang <kristian@hellang.com>
Date:   Thu Oct 23 21:31:45 2014 +0200

    Fixed ElasticSearch-net40 build

[33mcommit 0bf738b72c5a58e0d6f693ede788d23bd17da73a[m
Author: Kristian Hellang <kristian@hellang.com>
Date:   Thu Oct 23 21:27:05 2014 +0200

    Changed nuspec dependency to match packages.config

[33mcommit 93d2fec3c9a5ed4c3a4e3fa736d8676d8f17ee7c[m
Author: Kristian Hellang <kristian@hellang.com>
Date:   Thu Oct 23 21:19:50 2014 +0200

    Changed default value back to -1

[33mcommit 5a4e4a7fa8c15cd51a4abea0fe3f6587c4278c2c[m
Author: Kristian Hellang <kristian@hellang.com>
Date:   Thu Oct 23 21:12:01 2014 +0200

    Brought net40 project up to par

[33mcommit 29bea3cc390676ae9511aaf35bd1a48a149aff7f[m
Author: Kristian Hellang <kristian@hellang.com>
Date:   Wed Oct 22 12:53:23 2014 +0200

    Made startIndex ctor argument optional

[33mcommit 57d023672adf1ae632c720f1cb0d22792e0b26de[m
Author: Kristian Hellang <kristian@hellang.com>
Date:   Wed Oct 22 00:46:15 2014 +0200

    Removed EndIndex property

[33mcommit f58503fc87f5fc608cd5b7d8e713e642c7de0bd4[m
Author: Kristian Hellang <kristian@hellang.com>
Date:   Tue Oct 21 19:37:49 2014 +0200

    Added missing XML docs

[33mcommit fdeb4337efb9ee12993565996c8409b603aa10ef[m
Author: Kristian Hellang <kristian@hellang.com>
Date:   Tue Oct 21 19:18:43 2014 +0200

    Added more tests to improve coverage

[33mcommit 237099efb782b47d8052364e846e70eb1aba5c0c[m
Author: Kristian Hellang <kristian@hellang.com>
Date:   Tue Oct 21 19:12:10 2014 +0200

    Added indexes to test tokens

[33mcommit a4a111cd235083413ec8eb26c405a912c7d557d3[m
Author: Kristian Hellang <kristian@hellang.com>
Date:   Tue Oct 21 18:50:23 2014 +0200

    Use the new constructors when parsing

[33mcommit f3a7792bab2fd88d1a3a56c1a97a4b0c39d94598[m
Author: Kristian Hellang <kristian@hellang.com>
Date:   Tue Oct 21 18:48:34 2014 +0200

    Added StartIndex and EndIndex to MessageTemplateToken

[33mcommit 2cd1f187e44459459ebe7f3b2627c298cbf216c9[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Oct 23 07:55:49 2014 +1000

    Changelist

[33mcommit 3bbaa77974cc928ce96b9d2608dd8336317bd9bc[m
Merge: a4457ea9 fa50e20a
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Oct 23 07:53:36 2014 +1000

    Merge branch 'gmarz-master'

[33mcommit fa50e20a585bc2fbdd05b700f0a36b153a9f27a7[m
Merge: a4457ea9 a55135d9
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Oct 23 07:42:51 2014 +1000

    Merge branch 'master' of https://github.com/gmarz/serilog into gmarz-master

[33mcommit 64fb611610c9447b8f63e2c99b80f41859475860[m
Author: Ian Mercer <ianmercer@intentsoft.com>
Date:   Tue Oct 21 12:32:55 2014 -0700

    Fix a couple of 'CouchDB' references left in the MongoDB sink.
    Add a method that allows the MongoDB Sink to be configured with an existing Mongodatabase instance.
    
    This change allows a Mongodatabase to be used that is backed by a cluster of servers with failover and not just a single server on a single uri.

[33mcommit a55135d97dc9f356dc7cd8b700063c974983c9be[m
Author: Greg Marzouka <greg.marzouka@gmail.com>
Date:   Thu Oct 16 13:55:23 2014 -0400

    Rename ESPropertyFormatter -> ElasticSearchPropertyFormatter

[33mcommit 970730395dc83bb92bf58ecaff177b18ecd2b7f0[m
Author: Greg Marzouka <greg.marzouka@gmail.com>
Date:   Thu Oct 16 13:53:34 2014 -0400

    Build up bulk payload using a dictionary instead of string

[33mcommit e8f2b110d2ca02946d4a2f165113f749595d99b9[m
Author: Prasanna V. Loganathar <pvl@prasannavl.com>
Date:   Thu Oct 16 03:38:07 2014 +0530

    Update PCL with Profile259
    
    Include the new profile, with WP8.1 support.

[33mcommit a4457ea910be7dc0eb12963fd561babc39bdd850[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Oct 13 07:39:50 2014 +1000

    Changes.md

[33mcommit 2145111f4ca5f0acb4955259b888335d6106fddb[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Oct 13 07:35:22 2014 +1000

    Update NET40 build

[33mcommit 26b8405728f8b2f4eaf270a002c7e10445f91ddc[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Oct 13 07:33:13 2014 +1000

    Fixes #235 - ES scalar simplification for dictionaries, across all similar sinks.

[33mcommit 0e97fc05a3507ffe33c2c1bd9b4fdfc21af71922[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Oct 13 07:26:18 2014 +1000

    Fixes #240 - update TopShelf dependency.

[33mcommit 555962fbddb59a1db3629f161021e5965636dc49[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Oct 13 07:24:27 2014 +1000

    Fixes #243 - caching in AttributedDestructuringPolicy closes over first seen object of a type rather than using the passed parameter.

[33mcommit d3f026e6d56ae56d26504a66ab48f3e33d94476c[m
Merge: 53b03401 3456143c
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Oct 13 07:17:56 2014 +1000

    Merge pull request #231 from serilog/feature-adjustablelevels
    
    [Serilog.Sinks.Seq] Use "minimum accepted level" indicator to filter outbound events

[33mcommit 3456143c704c490d9778983014280d7d4485150b[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Oct 13 07:18:46 2014 +1000

    Fixed filtering to ensure excluded events are dropped from the batch, rather than causing early termination of the batch building process.

[33mcommit cede72378e01ebde181b70bae7212c5ea108d58d[m
Author: Greg Marzouka <greg.marzouka@gmail.com>
Date:   Sun Oct 12 16:16:47 2014 -0400

    Additional extension methods for configuring the Elasticsearch sink
    
    - Added ability to use a connection pool by passing a collection of URIs
    
    - Added overload that exposes the Elasticsearch.Net
      ConnectionConfiguration for full control of the connection to
      Elasticsearch

[33mcommit 80008cc1040589b33424c520f03c7b48318254ed[m
Author: Greg Marzouka <greg.marzouka@gmail.com>
Date:   Sat Oct 11 23:54:43 2014 -0400

    Swap NEST for Elasticsearch.Net

[33mcommit 53b03401f7d4333123446dba943b82bd4523840c[m
Merge: 74ef775e ce19891e
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Oct 10 07:44:27 2014 +1000

    Merge pull request #242 from nslowes/patch-1
    
    Update NLogSink to map Verbose level to Trace level in NLog

[33mcommit ce19891ef560b13e6775a63182aff7928f28f27d[m
Author: nslowes <nslowes@gmail.com>
Date:   Wed Oct 8 20:42:13 2014 -0400

    Update NLogSink.cs
    
    Mapped Verbose level messages to Trace in NLog.  They are both the "lowest" or most verbose log level so they seem to map naturally: https://github.com/NLog/NLog/wiki/Log-levels.  If this was omitted intentionally I would be interested in the reasoning.  Also corrected the default log message which indicated log4net instead of NLog.

[33mcommit 74ef775e8d0732ba960f0228b4be6aab68501d46[m
Merge: ed4fe1f9 1dfc2168
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Oct 9 07:16:32 2014 +1000

    Merge pull request #229 from pbolduc/master
    
    Created net40 version of Extras Topshelf.

[33mcommit ed4fe1f9a6ad422904bc52251418e2397a0d1e83[m
Merge: d7a1b18a 7c6d8a3e
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Oct 9 07:13:09 2014 +1000

    Merge pull request #236 from SEEK-Jobs/master
    
    Ensure dictionary keys are quoted in JSON even when they're numeric.

[33mcommit adc263c743b7142ca4320b910bb3e330a822bfcc[m
Merge: c98f427b d7a1b18a
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Oct 8 19:51:49 2014 +1000

    Merge branch 'master' into feature-adjustablelevels

[33mcommit d7a1b18a80f97b1d0caed3dbec295454179fe06d[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Oct 8 19:18:51 2014 +1000

    Fix the .NET 4.0 build

[33mcommit cb945495dda6a07290f39c425bd0de4ba5cd87f8[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Oct 8 19:07:10 2014 +1000

    Changelist

[33mcommit 093a85ee92ddeaea1a931386ec249597f278239e[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Oct 8 19:04:58 2014 +1000

    Fixes #238 - allow minimum level to be set dynamically.

[33mcommit 7c6d8a3e9dd37b33ab591cba8675f07344c04cae[m
Author: George Ma <gma@seek.com.au>
Date:   Fri Oct 3 09:37:55 2014 +1000

    When JsonFormatter formats a dictionary of <int, object>, the key should be double quoted.

[33mcommit 1dfc2168caba3a5d9eaf136fb9b62e08980a38a8[m
Author: Phil Bolduc <philbolduc@sierrasystems.com>
Date:   Wed Oct 1 08:52:25 2014 -0700

    Fixed DLL name and added pdb files to NuGet.

[33mcommit c98f427bab4d17374531d3017bfedaee989fa85d[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Oct 1 17:39:13 2014 +1000

    Reverted some accidental changes to the smoke test project.

[33mcommit 4031a4edea67be6690b936adfbec1a0b91722bf4[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Sep 30 08:16:38 2014 +1000

    Dynamically adjust the logging level of Seq sinks based on directives from the server.

[33mcommit 9d91e5642fb76343d23796aa6b1ca5a741498a6c[m
Author: Phil Bolduc <philbolduc@gmail.com>
Date:   Fri Sep 26 09:27:14 2014 -0700

    Created net40 version of Extras Topshelf.

[33mcommit 82bc4792996841b35bbf7787304e84e596dfc027[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Sep 27 07:59:21 2014 +1000

    Changelist.

[33mcommit a3f61180b4188684d83d8a07a4dc5339c57c8cca[m
Merge: 4fd8b121 5ba961b5
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Sep 27 07:52:29 2014 +1000

    Merge branch 'feature-206-batching'

[33mcommit 5ba961b5601090b392181b8fdaf7e115344eda9d[m
Merge: a58aca96 35c00214
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Sep 27 07:52:07 2014 +1000

    Merge branch 'master' into feature-206-batching
    
    Conflicts:
            src/Serilog.Sinks.AzureTableStorage/Sinks/AzureTableStorage/AzureBatchingTableStorageSink.cs

[33mcommit 4fd8b121d03228895d240b36221be3a06eb356ca[m
Merge: 35c00214 dcbdbb9b
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Sep 27 06:24:03 2014 +1000

    Merge pull request #226 from joshka/ensure-tablestorage-rowkey-unique
    
    Ensure Azure Tablestorage rowkey is unique

[33mcommit a58aca966cf696d1a394b412dfa98f231c81dfaa[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Sep 27 06:16:40 2014 +1000

    .NET 4.0 build support for new batching back-off strategy.

[33mcommit 5f72a8518b18903e03cbd5cb49bb2395985efbcd[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Sep 26 21:27:10 2014 +1000

    Maximum 10-minute backoff interval rather than 15, just to be on the "safer" side.

[33mcommit dcbdbb9b6b2603db9e2288c6bfc531daeab597e8[m
Author: Josh McKinney <joshka@server.fake>
Date:   Fri Sep 26 15:52:56 2014 +1000

    increment rowkeyindex as an atomic operation

[33mcommit 35c00214016bc404a6c3e8df330ca1d4161354c2[m
Merge: cf414a90 4fb615d8
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Sep 26 07:37:25 2014 +1000

    Merge branch 'cihanduruer-Net-4.0-support-for-Serilog.Sinks.ElasticSearch'

[33mcommit 4fb615d81efc4e6d9ec521eb363e016473b2a0f2[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Sep 26 07:37:09 2014 +1000

    By convention we keep packages.config files pointing to .NET 4.5.

[33mcommit 518a8453e5404636a0b3facd6bbaf9f5dccd25ff[m
Merge: cf414a90 2afea42b
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Sep 26 07:35:19 2014 +1000

    Merge branch 'Net-4.0-support-for-Serilog.Sinks.ElasticSearch' of https://github.com/cihanduruer/serilog into cihanduruer-Net-4.0-support-for-Serilog.Sinks.ElasticSearch

[33mcommit 5ec7758dc695b1e9a68a26ce1821349049c0ca78[m
Author: Josh McKinney <joshka@server.fake>
Date:   Wed Sep 24 14:54:31 2014 +1000

    Ensure that row key is unique by appending an incrementing index. Fixes #225

[33mcommit fbcf9ca617b9c6565aba2bf8f4cfe74f056da5f8[m
Author: Josh McKinney <joshka@server.fake>
Date:   Wed Sep 24 14:45:40 2014 +1000

    minor - fix whitespace

[33mcommit cf414a90fec36ba17349e34ee8b8c9053166a938[m
Merge: 7a7ce820 d80ca765
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Sep 24 06:42:57 2014 +1000

    Merge pull request #224 from mcintyre321/master
    
    Use UTC to generate partitionkey

[33mcommit d80ca765a4e22a05578bfc933439fada229518da[m
Author: mcintyre321 <mcintyre321@gmail.com>
Date:   Tue Sep 23 13:09:21 2014 +0100

    Use UTC to generate partitionkey

[33mcommit 2afea42b5826bab40720d59f444ddfcadbd637bd[m
Author: Cihan Duruer <cihan.duruer@gmail.com>
Date:   Tue Sep 23 09:00:27 2014 +0200

    Forgotten project file is added

[33mcommit 9e2f30fcf916913a59783b1285661745fd8c4555[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Sep 23 08:34:03 2014 +1000

    A simple exponential backoff mechanism for PeriodicBatchingSink

[33mcommit 615f99f1bf189b5264fa105c5c5c9d8cfd6a5f80[m
Author: Cihan Duruer <cihan.duruer@gmail.com>
Date:   Mon Sep 22 11:36:34 2014 +0200

    .Net 4.0 support for Serilog.Sinks.ElasticSearch
    
    Serilog.Sinks.ElasticSearch .Net 4.0 distribution is added to solution.
    Serilog.Sinks.Seq package.config file was missing in the main
    repositories, so its added.

[33mcommit 7a7ce820fac95e7889ba0e67a0d11816493cf843[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Sep 17 21:45:59 2014 +1000

    Changelist, plus addendum to fix NUSPEC in #216

[33mcommit 18849f3b54e88e393e4d3eb792432a45bcfd0d22[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Sep 17 21:41:00 2014 +1000

    Check for null identities in the ASP.NET UserNameEnricher (fixes #213)

[33mcommit b8e0165929ddd283bb3d34b9b26e8160ed85b6f2[m
Merge: b536972e 966f75dd
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Sep 17 21:29:13 2014 +1000

    Merge pull request #216 from merbla/master
    
    Updated to use Splunk TCP Writer

[33mcommit 966f75ddf2f10d820fb7764f6834ed2f0c8d94bd[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Tue Sep 16 20:34:22 2014 +1000

    Updated to use Splunk TCP Writer

[33mcommit b536972ea411aa4f5b12cfb5ccc7a40657635e7d[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Sep 11 15:16:32 2014 +1000

    Changelist.

[33mcommit 31018a467e8597f96d32359834d52e2b6d809c4a[m
Merge: 94474e77 f5c4f307
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Sep 11 15:01:58 2014 +1000

    Merge pull request #204 from skkeeper/master
    
    Added basic support for HTTP Auth in the CouchDB Sink

[33mcommit 94474e7780d650ac5e158b8a1b14d5a8e223c27b[m
Merge: f9a42b05 6eef44a0
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Sep 11 15:00:34 2014 +1000

    Merge pull request #208 from khellang/patch-1
    
    Fixed typo

[33mcommit f9a42b05c320d1c288a6c252b86af4d441bf2429[m
Merge: 4c156bed b4da6ff0
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Sep 11 14:59:18 2014 +1000

    Merge pull request #209 from merbla/master
    
    Updated nuspec for Splunk Sink to match GA release of Splunk SDK

[33mcommit 4c156bed0d606973b6f688215d5d17b8f6360376[m
Merge: 38407798 10fb7865
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Sep 11 14:58:40 2014 +1000

    Merge pull request #211 from khellang/patch-2
    
    Added missing format placeholder

[33mcommit 10fb7865ba843f83789125925f9c7bcad9392877[m
Author: Kristian Hellang <kristian@hellang.com>
Date:   Thu Sep 11 01:39:11 2014 +0200

    #210: Added missing format placeholder

[33mcommit b4da6ff01899969eb504f8fba6d050145425815e[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Wed Sep 10 08:23:37 2014 +1000

    Corrected issue with NuGet package update

[33mcommit de6b355baf851773fc3bf1e4b8db1dd50ee7217f[m
Merge: 7b00aa30 09789953
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Wed Sep 10 08:19:21 2014 +1000

    Merge remote-tracking branch 'origin/master'

[33mcommit 7b00aa3045b3cbfbef01ff03282b64feb5bb2f36[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Wed Sep 10 08:16:25 2014 +1000

    Revert "Removed old pre spec ready for GA release"
    
    This reverts commit a3e27e3b682db0591953ba7f147adacdb7f739bd.

[33mcommit 0978995330f47993922ab63bf41f66478926c364[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Wed Sep 10 07:46:38 2014 +1000

    Updated to match GA release

[33mcommit 6eef44a0c070d4d13a85fd83b218d32cb2926074[m
Author: Kristian Hellang <kristian@hellang.com>
Date:   Tue Sep 9 19:58:37 2014 +0200

    #207: Fixed typo

[33mcommit a3e27e3b682db0591953ba7f147adacdb7f739bd[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Fri Sep 5 17:15:12 2014 +1000

    Removed old pre spec ready for GA release

[33mcommit f5c4f30716ab1e7e31c945be12f8a005f0c5966b[m
Author: Fábio André Damas <skkeeper@gmail.com>
Date:   Wed Sep 3 10:14:25 2014 +0100

    Improved the handling of the authentication parameters on the CouchDB sink.

[33mcommit ed53a3864e0ebdcc16effc348b9ac4c9bc25cac1[m
Author: Fábio André Damas <skkeeper@gmail.com>
Date:   Tue Sep 2 19:32:54 2014 +0100

    Added basic HTTP authentication support to the CouchDB sink.

[33mcommit 384077980735fab2a4b6168f7c7a218cb266e7e9[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Sep 1 19:05:20 2014 +1000

    Picked up a couple of ReSharper warnings in Serilog.Sinks.Splunk.

[33mcommit e0143d6b411a0718c573eebb356f63724f775f49[m
Merge: c03cb81c d4d13ba9
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Sep 1 18:53:19 2014 +1000

    Merge branch 'mcintyre321-master'

[33mcommit d4d13ba9a25feae13c8c5ac8494a1cadbc74dbb7[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Sep 1 18:52:56 2014 +1000

    Couple of minor tweaks to keep R# happy, provide batching for other AzureTableStorageSink overload.

[33mcommit 8b1aa0b24318ba3dcdbc4e606addbe95029f98e6[m
Merge: c03cb81c 988a2ba9
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Sep 1 18:43:12 2014 +1000

    Merge branch 'master' of https://github.com/mcintyre321/serilog into mcintyre321-master

[33mcommit c03cb81c1b7fe35d934f72c4e7c3c2f197ed526d[m
Merge: d0091c85 45e85b26
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Sep 1 18:41:02 2014 +1000

    Merge branch 'jbattermann-master'

[33mcommit 45e85b26148c5a8ad04c52c6252932baa5baac9d[m
Merge: d0091c85 1928fc1b
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Sep 1 18:39:31 2014 +1000

    Merge branch 'master' of https://github.com/jbattermann/serilog into jbattermann-master

[33mcommit d0091c8531d81634267e863b4e32bf39e2d39a59[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Aug 31 13:18:47 2014 +1000

    Reintroduced fix from https://github.com/continuousit/seq-client/pull/19 dropped in project move

[33mcommit 988a2ba902d1200a10fdcebdbdb90b9bc6f799b6[m
Author: mcintyre321 <mcintyre321@gmail.com>
Date:   Fri Aug 29 11:22:25 2014 +0100

    Prevent duplicate partition keys

[33mcommit ccf6e8b4dce2ea2f72215f3a328c9460d44fcba4[m
Author: mcintyre321 <mcintyre321@gmail.com>
Date:   Fri Aug 29 10:22:58 2014 +0100

    Add batch row id to RowKey to ensure uniqueness

[33mcommit aecbc9b1d1545ac0353357c6505d5f9cd1b18cc2[m
Author: mcintyre321 <mcintyre321@gmail.com>
Date:   Fri Aug 29 10:02:05 2014 +0100

     - Changed configuration to be inline with other sinks
     - Use min timestamp as partition key

[33mcommit 71068c3a94224236562d51e876c5494bd77f049d[m
Author: mcintyre321 <mcintyre321@gmail.com>
Date:   Thu Aug 28 16:48:16 2014 +0100

    Added batching sink for Azure table storage

[33mcommit 22eac361cf3b09621c79bce657602109180ed222[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Aug 27 20:37:03 2014 +1000

    Use a hacky 'pre-' disambiguator on the Splunk sink's NUSPEC file so that it can be packed by a different build step, using the -pre prerelease version tag. NuGet won't allow it to have a dependency on the -beta SDK unless it's a pre-release itself.

[33mcommit 827d28e95c4e73eb3df158595a7e923d6c2c1fc3[m
Merge: fb78e80c 76b3eb72
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Aug 27 17:55:37 2014 +1000

    Merge branch 'merbla-master'

[33mcommit 76b3eb72721e7430af26e9b415838b9c51940aa2[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Aug 27 17:55:25 2014 +1000

    Changelist update

[33mcommit d413778f0f53a8f15d9eb92b01f311ada0a63ea3[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Aug 27 17:35:59 2014 +1000

    Clobbered a couple of build warnings

[33mcommit f3a600b3d0fb17f17a6ff068d1af524ea8f74ad9[m
Merge: fb78e80c bef83c48
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Aug 27 17:32:12 2014 +1000

    Merge branch 'master' of https://github.com/merbla/serilog into merbla-master
    
    Conflicts:
            Serilog.sln
            src/Serilog.Sinks.Splunk/Sinks/Splunk/SplunkSink.cs

[33mcommit fb78e80c60999a8efdad1b71ac630f9a17d7bd35[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Aug 27 17:24:46 2014 +1000

    Fix NUSPEC icon/project links.

[33mcommit d5cbf2c699af8347c4bdf1bb185a6fb20e7ed378[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Aug 27 17:23:31 2014 +1000

    Includes a sink for Seq that will supersede the one shipped as part of the Seq project.
    
    Makes HttpLogShipper etc. available to factor out and share between Serilog projects. Brings the .NET 4.0 JSON formatter into line with the portable one. Adds support for including property renderings in the JSON document if they're included in the message template.

[33mcommit bef83c4853ce46dd0450a9c7bc1accc5106db33e[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Wed Aug 27 17:04:36 2014 +1000

    Removed JSON.Net and changed to inbuilt json formatter

[33mcommit d3736444dcc98b0f8b487adf094c5f14d0e69718[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Wed Aug 27 09:38:21 2014 +1000

    Added Json.Net and extended properties logging

[33mcommit f03e883510e5a5c81ce20eeaa15a95d77996c543[m
Merge: 51ef359a fff8daf9
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Aug 25 18:26:45 2014 +1000

    Merge branch 'mattfurness-property-policy'

[33mcommit fff8daf9145e1fadf7b8dfaebc6a21f087d075e0[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Aug 25 18:26:16 2014 +1000

    A couple of consistency changes, removed some leftover files from an earlier rename.

[33mcommit 69516ee41d4088196425be1021c0490e2c29e105[m
Merge: 51ef359a 2d9eac9b
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Aug 25 18:19:21 2014 +1000

    Merge branch 'property-policy' of https://github.com/mattfurness/serilog into mattfurness-property-policy

[33mcommit 51ef359aa19ade1825c39f9fb7a0a037ecc903f2[m
Merge: 78f363d4 f769ccf7
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Aug 24 07:10:03 2014 +1000

    Merge branch 'joneal-master'

[33mcommit f769ccf76b76e5ae598f8f4c37fe3034ec171c31[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Aug 24 07:09:31 2014 +1000

    Updated CHANGES

[33mcommit 7e906b32c8721404220bcfbac41896e6c94b2985[m
Merge: 78f363d4 50634ded
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Aug 24 07:02:42 2014 +1000

    Merge branch 'master' of https://github.com/joneal/serilog into joneal-master

[33mcommit 78f363d42ee380027633c394e8acfa830ae8f447[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Aug 23 20:39:34 2014 +1000

    Markdown a better format for the changelist.

[33mcommit 3b3fb5317e8041377545a7ea1de1c181b5963a13[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Aug 23 20:38:40 2014 +1000

    Added CHANGES (in need of retrospective population)

[33mcommit 570ffe47f6ed26e484a6670d28863fa1471fe15c[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Aug 23 20:22:57 2014 +1000

    Introduces LogContext.Suspend() to fix cross-domain calls where the remote AppDomain cannot load the Serilog assemblies. Fixes #196.

[33mcommit 50634deda43757b7427949167d5798d27fccdaeb[m
Author: Jerry ONeal <jerry.oneal@samtec.com>
Date:   Fri Aug 22 09:34:22 2014 -0400

    Added parameter 'manageEventSource' to allow sink consumer to select whether the EventLog sink automatically checks/creates the event source.

[33mcommit 8bac744706dae71f1855d6f072dc45ce77032399[m
Author: Jerry ONeal <jerry.oneal@samtec.com>
Date:   Fri Aug 22 09:34:08 2014 -0400

    Added parameter 'manageEventSource' to allow sink consumer to select whether the EventLog sink automatically checks/creates the event source.

[33mcommit ab7b4073ff7c4709870bb2d53c1220dc476f6168[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Mon Aug 18 21:20:51 2014 +1000

    Updated sink to use batchsize and interval

[33mcommit 31733e8ef712fbb932d814a171ef049caa1f638b[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Mon Aug 18 21:20:13 2014 +1000

    Updated sink to check on interval basis when using HTTP

[33mcommit 1928fc1b40fb175c3cfd4ed3c5af8614607f035c[m
Author: Jörg B. <jb@joergbattermann.com>
Date:   Sat Aug 16 23:21:04 2014 +0200

    Added option to en-/disable HTTP Proxy Check

[33mcommit bf4f6bed1ea45f64f96d9a8141a1a840b1ff59d8[m
Author: Jörg B. <jb@joergbattermann.com>
Date:   Sat Aug 16 22:49:37 2014 +0200

    Fixed typo in code comment. meh.

[33mcommit 6244fdc6783aeb48c74add0d1d3c360205341a3b[m
Author: Joerg Battermann <jb@joergbattermann.com>
Date:   Sat Aug 16 22:47:21 2014 +0200

    Updated HttpRequestClientHostIPEnricher to also check for proxies, if available

[33mcommit 384e5dc5b78a0fbe8b941784d164accf89077f0f[m
Author: Joerg Battermann <jb@joergbattermann.com>
Date:   Sat Aug 16 22:27:16 2014 +0200

    Added a couple more HttpRequest* Enrichers
    
    Contains Client IP, -Hostname, User Agent, (Raw) Url, Referrer and
    Request Type. Never hurts to have a little extra options and info at
    hand.

[33mcommit f92603b1249736d758ea8632199ea7ecbc9a4d09[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Tue Aug 12 10:14:03 2014 +1000

    Moved file to correct location

[33mcommit 2d9eac9b11518d080c61555e1e5291015efb4d5b[m
Author: Matt Furness <matt.furness@gmail.com>
Date:   Tue Aug 12 09:19:57 2014 +1000

    rename extra to DestructureByIgnoring, add expression validation, add tests, general code improvements

[33mcommit 8fdeb2ea3138947da96219a39ddc50df4df9494d[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Tue Aug 12 09:07:32 2014 +1000

    AssemblyInfo and Namespace cleanup for PR

[33mcommit 59d70e85187f9c7211d46143e2a302461d13d77e[m
Merge: 775be195 621c7e0d
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Aug 5 08:18:46 2014 +1000

    Merge pull request #184 from ChrisHimsworth/master
    
    (Splunk Sink) - fix use of the receiver.Attach()

[33mcommit 775be19574bbd564177351a32cd11f406fc13252[m
Merge: dae307c1 98576a8f
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Aug 5 08:16:48 2014 +1000

    Merge pull request #168 from mattfurness/raygun-data-names
    
    (Raygun Sink) - rename built-in properties to reduce clashes

[33mcommit dae307c145a6867fe1f278c09e24eed3e37fa890[m
Merge: 6c7dcf20 ad8ec872
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Aug 5 08:14:35 2014 +1000

    Merge pull request #186 from joneal/master
    
    Upgraded Topshelf to 3.1.122.0

[33mcommit ad8ec872bb3c093a15fbedea6682cbf20d469544[m
Author: Jerry ONeal <jerry.oneal@samtec.com>
Date:   Mon Aug 4 10:00:42 2014 -0400

    Upgraded Topshelf to 3.1.122.0

[33mcommit 98576a8f56b1d4e1a848bcc3e99551fda242ec1d[m
Author: Matt Furness <matt.furness@gmail.com>
Date:   Mon Aug 4 20:59:14 2014 +1000

    Change Raygun Serilog message property keys to be less jarring

[33mcommit 621c7e0da860f13a983381640fb528904f15935c[m
Author: Chris Himsworth <chrishims@hotmail.com>
Date:   Mon Aug 4 10:47:07 2014 +0100

    Corrected the use of the Splunk API
    
    To use receiver.Attach, you must use the returned Stream object to post
    further data.

[33mcommit 5113f5f2d1c92ff8b6381f277e5df646d35173ae[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Fri Aug 1 21:39:12 2014 +1000

    Added PCL and FullNexFX assembly with TCP

[33mcommit 218935e828538294fdd9bb45fcd83d2749413bce[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Fri Aug 1 21:30:45 2014 +1000

    Removed old assembly

[33mcommit 6c7dcf208f29370b11226fb7adb149cb03133b74[m
Merge: f6554053 c435f53c
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Jul 31 08:03:02 2014 +1000

    Merge pull request #167 from mattfurness/custom-tags
    
    Support additional tags during Raygun sink configuration.

[33mcommit f655405357805202ed196c56726c9a5b4f4f8249[m
Merge: b989a351 0d3e73f9
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Jul 31 08:01:30 2014 +1000

    Merge pull request #175 from serilog/commonGetPropertyDetection
    
    Share "structure property" detection code between components that require it.

[33mcommit b989a351c620994cc187becdd528242fd07b98b9[m
Merge: 47577ed4 36f6acc1
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Jul 31 07:55:06 2014 +1000

    Merge pull request #176 from joshka/master
    
    Make AzureTableStorage sink work with AppSettings.

[33mcommit 36f6acc1c525f8d6c7744ae5d382ed1663b15ead[m
Author: Josh McKinney <joshka@server.fake>
Date:   Wed Jul 30 11:28:39 2014 +1000

    Make AzureTableStorage sink work with AppSettings.
    
    Adds an overload to the AzureTableStorage extension
    method that takes a connection string rather than just
    the storage object. This allows configuration similar
    to the following to just work:
    
        <add key="serilog:using" value="Serilog.Sinks.AzureTableStorage" />
        <add key="serilog:write-to:AzureTableStorage.connectionString" value="UseDevelopmentStorage=true;" />

[33mcommit 0d3e73f93185c99fc7491228f6137e06950b2dd5[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Tue Jul 29 21:13:00 2014 +1000

    use common prop code in PropertyValueConverter

[33mcommit 6e1225b383503d80a8f0283f735d54d5a4a34abd[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Tue Jul 29 17:52:28 2014 +1000

    use common property detection

[33mcommit 47577ed4ed09431f77e8c3ee6010e691e4137594[m
Merge: 6aa9a1f1 0d544f9e
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Jul 28 18:53:17 2014 +1000

    Merge pull request #173 from elmahio/master
    
    Upgraded the elmah.io package

[33mcommit 0d544f9ea6a9fcac11ceed650cece6403d2c5b72[m
Author: ThomasArdal <thomasardal@gmail.com>
Date:   Mon Jul 28 08:48:54 2014 +0200

    Replaced dependency from elmah.io package to elmah.io.core. The elmah.io package contain web.config transformations, which is not needed in this usage. I've also upgraded the package to the most recent version, which contains some nice bugfixes.

[33mcommit 6aa9a1f1c76275b860b86164b4973fba070d2223[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Jul 27 08:35:12 2014 +1000

    Additional catch block to handle destructuring objects with indexer properties named other than "Item".

[33mcommit acc340b4b3cc7358d2ea24466c26c43ab6cb688e[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Jul 25 10:41:06 2014 +1000

    .NET 4 version of #170

[33mcommit e3f60d4cb695fd64fa235395b7f4ab031031c497[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Jul 25 10:19:47 2014 +1000

    Fixes #170 - failure when destructuring objects with indexers.

[33mcommit d8becf30a46a4dc0bab40f155328c15516c5d83d[m
Author: Matt Furness <matt.furness@gmail.com>
Date:   Mon Jul 21 19:00:49 2014 +1000

    add Lambda Ignore extensions

[33mcommit 1e81f45ab12b59c6ccee5cc37704d3b8aa8cbfc8[m
Author: Matt Furness <matt.furness@gmail.com>
Date:   Sun Jul 20 21:56:43 2014 +1000

    fix duplicate variable name

[33mcommit 57c09253387b765ee6f8418d62306a40e229e1b6[m
Author: Matt Furness <matt.furness@gmail.com>
Date:   Sun Jul 20 21:51:49 2014 +1000

    add ability to ignore properties using a lambda expression when destructuring

[33mcommit c682839882850890711495ba979b1b6d3a34c9ec[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Jul 20 12:39:18 2014 +1000

    Updates the Serilog ElasticSearch sink to NEST 1.0. (Internalizes some previously public types to enable moving to shared Serilog implementations.)

[33mcommit b3dd40120f87ff0278d6225731ef836d8a04fe09[m
Author: Matt Furness <matt.furness@gmail.com>
Date:   Sat Jul 19 19:35:01 2014 +1000

    Prefix ragun properties with Serilog$ to decrease likelyhood of key clashes

[33mcommit c435f53c55a760c90bd4a440b69a2f5e8262f334[m
Author: Matt Furness <matt.furness@gmail.com>
Date:   Sat Jul 19 19:21:46 2014 +1000

    Support additional tags during Raygun sink configuration.

[33mcommit e36de1a5d5455f40c2052d4b0ef866126d26050d[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Jul 17 22:22:57 2014 +1000

    Adds the Serilog.Extras.Attributed package.

[33mcommit 7739f8b4f0a0d46d42564f1300d72882a6179cbb[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Jul 17 20:42:41 2014 +1000

    Adds serilog:enrich:with-property:PropertyName syntax to the app settings extension.

[33mcommit 425ba8c73add54e0d9025e19c42934512fcf88a7[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Jul 17 20:25:21 2014 +1000

    Fixes #164 - convert enum values properly when read from app settings.

[33mcommit 43ec02149d17cab08b0ee4847c3a1b08bfbd43a3[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Jul 17 20:18:57 2014 +1000

    Fixes #145 - always pass a logger name through to log4net's LogManager.GetLogger() call; provide the defaultLoggerName: option on the WriteTo.Log4Net() method to override the default "serilog".

[33mcommit 9aea68e70f09edc1f0495f081931642116b9a897[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Sun Jul 6 12:23:48 2014 +1000

    remove redundant code

[33mcommit 7b6df8c7bdb46d94ba7383cc9a7c43b7609d901e[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Jun 30 21:47:58 2014 +1000

    Fixed reference paths in the NLog sink, addressed a couple of minor consistency issues.

[33mcommit a1b19e70d37318a509593a25a18f4ce5cb0450e2[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Jun 30 21:15:50 2014 +1000

    Added a link to the samples repo

[33mcommit 436e958e7c9b4fef7ec0b34bc70a2a4b58e4cfc7[m
Merge: 1722ae05 7f66a264
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Jun 21 15:36:02 2014 +1000

    Merge pull request #153 from ruba1987/feature/nlog-support
    
    [Feature] NLog sink support

[33mcommit 7f66a2642e01d835ef576c3e3fc28ad9dacf4c4a[m
Author: ruba1987 <russell.j.baker@gmail.com>
Date:   Fri Jun 13 16:22:27 2014 -0400

    Added NLog sink support
    
    Author:    ruba1987

[33mcommit 1722ae052d2bb262546392a3f050512c066a1ea7[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Jun 19 22:08:03 2014 +1000

    Fixes #152 - convert null/empty strings appropriately when setting Nullable<T> values from <appSettings>.

[33mcommit 402a5d45b82ba42f725741e7a7dc3bc58b7210b7[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Jun 19 17:19:41 2014 +1000

    Test .csproj cleanup (fixes #139).

[33mcommit 04c223c600f02156ec8adea3505ae441e5e0984b[m
Merge: 0493647c ffa3f72b
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Jun 19 17:12:43 2014 +1000

    Merge branch 'master' of https://github.com/serilog/serilog
    
    Conflicts:
            Serilog.sln

[33mcommit 0493647caac7a2b4d8e5aa9ad22e40e94de881e7[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Jun 19 17:06:24 2014 +1000

    Support :u and :w as format specifiers for UPPERCASE and lowercase output respectively; only effective in output templates. Fixes #88 and #151.

[33mcommit ffa3f72b266d93a57e0140af4e0b14f2e92fb5c0[m
Merge: 84c0810d 6b85db01
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Jun 18 21:34:24 2014 +1000

    Merge pull request #149 from jbattermann/master
    
    Added Sink for Microsoft Application Insights for Visual Studio Online

[33mcommit 84c0810d6e5f9bd5ab4ae1cc13b3407eddffd817[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Jun 9 10:25:16 2014 +1000

    Fixes #146 - use a culture-neutral format for the default console output template.

[33mcommit 6b85db01493d82da022b4cb7a0d20a4882871909[m
Author: Joerg Battermann <jb@joergbattermann.com>
Date:   Sun Jun 8 16:54:10 2014 +0200

    Added support / Sink for Microsoft Application Insights

[33mcommit 7dbbd16429e4deba661e1fcc82ee3df9ce10609f[m
Merge: aca89d92 e1985a67
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed May 21 20:35:19 2014 +1000

    Merge pull request #143 from merbla/master
    
    Updated to version 1.0.2 of Splunk SDK

[33mcommit e1985a678b5fcc736f894940846d3badcdbb1273[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Wed May 21 20:12:50 2014 +1000

    Updated to version 1.0.2 of Splunk SDK

[33mcommit aca89d926efd8961645443a70403b0e96de797e1[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Wed May 21 07:39:11 2014 +1000

    Fixed .NET 4 project.

[33mcommit 73231341226fcb24533f40f176eb3fef9c5accba[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sun May 18 20:11:39 2014 +1000

    Strings in .NET aren't (at build time) IEnumerable<char> anymore. Rather than use ToCharArray() and allocate more junk, switch to indexed access.

[33mcommit 49efd3fbc38c7b4a95553df05365dc6207af1c61[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sun May 18 20:09:06 2014 +1000

    Tiny refactor to cut some duplication out from LogContext.

[33mcommit 2cfe7a0ec5f98b2a37e1327db7541893a64637db[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sun May 18 20:08:14 2014 +1000

    Knock out some minor style inconsistencies picked up by ReSharper inspection.

[33mcommit 197f80d501e505eac19fa64133856b20c8f4eec0[m
Merge: efcf82f2 ecffa874
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun May 18 12:46:13 2014 +1000

    Merge pull request #142 from Jaben/MultipleProperties
    
    Multiple Property for LogContext

[33mcommit ecffa874f9f18359393b3d945e41aaae0026934f[m
Author: Jaben <jaben@yetanotherforum.net>
Date:   Sat May 17 20:57:06 2014 -0400

    Fixed typos and added contracts checks for (now) public API. Updated for namespace conventions.

[33mcommit efcf82f26d60cf141a22bbaedeb6e38fd87da5a9[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Sun May 18 10:04:53 2014 +1000

    spelling errors

[33mcommit 8753ca4a09dd7d6ced21dd895bd8ed01da098426[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Sun May 18 10:04:15 2014 +1000

    explicit private

[33mcommit 27520c9d2c0bb202cb975d686c0aae9cc9ff8acb[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Sun May 18 10:00:25 2014 +1000

    delete assembly info from tests
    
    no point for a test assembly

[33mcommit a3e74b08123c295a63bfd8cd810bb6a55b61bf77[m
Author: Jaben <jaben@yetanotherforum.net>
Date:   Sat May 17 18:11:49 2014 -0400

    Referencing closed issue #126.
    Renamed LazyFixedPropertyEnricher to PropertyEnricher and added LogContext.PushProperties(params PropertyEnricher[] properties) method. PropertyEnricher can now be used with ForContext() as well.

[33mcommit 013d42def3062cd45071d5c2c0783482889e63fb[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Sat May 17 13:13:42 2014 +1000

    move samples
    
    they are now here https://github.com/serilog/serilog-samples/

[33mcommit 3c086a35d3a1238951d4b131ae3ae4afe304e039[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Sat May 17 12:21:19 2014 +1000

    prefer usings outside namespace

[33mcommit fa9357b1b1fa05c88c3e23fe0994b601babf9993[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Sat May 17 12:17:05 2014 +1000

    add r# settings file

[33mcommit 57bba3936e4f102ccb78d1a2fa61a3dca1c515bc[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Sat May 17 12:15:11 2014 +1000

    incorrect param name in arg exception

[33mcommit 08e1673bac3aba1a9604714a957a228c5c728c94[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Sat May 17 12:13:53 2014 +1000

    redundant variable init

[33mcommit 322658896a1603e0b67cc76a2547ef740374a08f[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Sat May 17 12:13:03 2014 +1000

    redundant field init

[33mcommit 7d1456e1c80782bd91bba50554f432a6d021f2d8[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Sat May 17 12:12:39 2014 +1000

    better variable naming

[33mcommit 54ccfc891c4e7e064ac61635833985c03f7b9b4a[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Sat May 17 12:12:25 2014 +1000

    remove redundant cast

[33mcommit b0690bd74b86ff3ef7fd5ee3079078f74010f31e[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Sat May 17 11:32:19 2014 +1000

    implicit private

[33mcommit 2e553dcb261326cefc65a7de8b9b17dd545b2136[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Sat May 17 09:53:41 2014 +1000

    no more regions

[33mcommit 9a473b3770e86bfd459158c74cc8d2f668007bd7[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Fri May 16 21:56:17 2014 +1000

    var all the things

[33mcommit e8788f43a8a33defd14b831484ef891794202af9[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Fri May 16 21:34:23 2014 +1000

    fixe some spelling errors

[33mcommit 9441ceab2d50239928c7a6d3ef31fbc953e90ecf[m
Merge: cb9848ee 1bdbb848
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Fri May 16 09:51:32 2014 +1000

    Merge pull request #141 from serilog/redundantUsings
    
    remove redundant usings

[33mcommit 1bdbb848eb4d3ba5cdf33d8d1697490a8879ff30[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Thu May 15 21:02:20 2014 +1000

    remove redundant usings

[33mcommit cb9848ee8a4db6b03c883b422f8e2a3a9d5b0ddf[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Thu May 15 08:47:49 2014 +1000

    Fixed the assembly name for the Raygun sink - #132

[33mcommit 54f23607b9bd1ba9d540411f86b519f2dcfac9ea[m
Merge: 24673be4 fc991dba
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Thu May 15 08:30:49 2014 +1000

    Merge branch 'mivano-raygun'

[33mcommit fc991dbadeb1f62e731203ead8cad2fd6e3f2fce[m
Merge: 24673be4 82030f43
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Thu May 15 08:30:34 2014 +1000

    Merge branch 'raygun' of https://github.com/mivano/serilog into mivano-raygun
    
    Conflicts:
            Serilog.sln

[33mcommit 24673be408fe8851784df9ec0f828f0c7147c34a[m
Merge: 4302f618 02a7e79c
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu May 15 08:13:08 2014 +1000

    Merge pull request #137 from SimonCropp/master
    
    dont check in nuget.exe

[33mcommit 4302f618d307f41fc7443fee3277d3d9ba1e038b[m
Merge: 90167d88 feb5a682
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu May 15 08:12:54 2014 +1000

    Merge pull request #138 from abrussak/master
    
    Enhanced Email Sink with Port & SSL

[33mcommit 90167d885d18b876d63fb9cc366ac6248d41e073[m
Merge: 8740be4c ce5b4af0
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu May 15 08:11:23 2014 +1000

    Merge pull request #140 from ShawInnes/master
    
    Fix bug with Logentries handling of multi-line messages

[33mcommit 8740be4c141b86351c7b3f9e51bbea02e87d8ff7[m
Merge: 9df7bb42 a5acf0a2
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu May 15 08:09:24 2014 +1000

    Merge pull request #133 from mivano/counter-gauge-metrics
    
    Counter and gauge metrics

[33mcommit a5acf0a26710a4573a7cb6a6094d48ecfce44e29[m
Merge: ecf14131 9df7bb42
Author: Michiel van Oudheusden <m.vanoudheusden@gmail.com>
Date:   Wed May 14 23:12:42 2014 +0200

    Merged master into this branch

[33mcommit ce5b4af0b73483bff0e828b1222c9a2d713bbb1e[m
Author: Shaw Innes <shaw@immortal.net.au>
Date:   Wed May 14 22:34:47 2014 +1000

    LogEntries Sink does not correctly log exceptions due to a line termination issue.

[33mcommit e553a5eddfa7c3df7a5e7da0ae446b7e7aaaf4f9[m
Merge: 67425dd0 9df7bb42
Author: Shaw Innes <shaw@immortal.net.au>
Date:   Wed May 14 22:11:56 2014 +1000

    Merge pull request #1 from serilog/master
    
    Merge from master

[33mcommit feb5a682a3f074437423a42068e4582b9a02bec8[m
Author: Alex Russak <abrussak@gmail.com>
Date:   Mon May 12 12:21:26 2014 -0700

    Spelling mistake
    
    Yay! Spelling!

[33mcommit 18b2bc5a27c45049e72c9dae59ef10fab10ddbde[m
Author: Alex Russak <abrussak@gmail.com>
Date:   Mon May 12 12:16:17 2014 -0700

    Added class to handle various Email connection settings.
    
    Office365 required me setting the Port to another value and also
    enabling SSL.
    
    Added a new class 'EmailConnectionInfo' that contains properties used by
    the sink instead of direct parameters.
    Original extension method still works and now just constructs a
    connection info and passes that through.

[33mcommit 02a7e79cf154cf45a68382c11fcfde3e5f98daf6[m
Author: Simon Cropp <simon.cropp@gmail.com>
Date:   Mon May 12 08:50:22 2014 +1000

    dont check in nuget.exe
    
    part of #136

[33mcommit 9df7bb4215cb433cc2149cbb58f8e04def4680b9[m
Merge: 5346b91c 4f73d6b7
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sun May 11 21:33:43 2014 +1000

    Merge branch 'master' of https://github.com/serilog/serilog

[33mcommit 5346b91c754f830b2300a41640eb599ae2575ab9[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sun May 11 21:33:24 2014 +1000

    Brought a couple of conventions in the Splunk sink in line with the other sinks. Obsoleted the interface-based configuration type; using the concrete type will be better for evolution. Seems like there's a good opportunity to use JsonFormatter to get more interesting data into this one.

[33mcommit 7a0c958ff81acfaf5bc92fafb76c1d9213ba21e8[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sun May 11 21:29:37 2014 +1000

    Suppressed a couple of distracting but unimportant warnings in sample code.

[33mcommit c73e8d459931b96f57554135dc53894b55e073bc[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sun May 11 20:56:20 2014 +1000

    Part 1 of #116 - ignore missing properties in output templates, default to "l" (literal formatting) for strings unless other formatting is specified.

[33mcommit 4f73d6b7bfcda20f102ab520cc06c9e4348d43f3[m
Merge: d96326a6 ce5d4a2b
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri May 9 20:36:27 2014 +1000

    Merge pull request #135 from graemefoster/TraceSinkForAzureWebsite
    
    Trace sink fix for Azure websites

[33mcommit ce5d4a2b852b53953addaf8359e14b10d8fb4262[m
Author: Graeme Foster <graemefoster@gmail.com>
Date:   Fri May 9 12:31:24 2014 +0800

    Azure website diagnostics for some inexplicable reason won't pick up Trace.Write even when it has a newline. But it's fine with Trace.WriteLine

[33mcommit d96326a6d90faad884f039fa44cd1eb355c3c093[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Wed May 7 10:00:08 2014 +1000

    Fixed the .NET 4.0 build.

[33mcommit 8353a81cb6a03d52338d34861ffc6213579824f3[m
Merge: 5ce3330e 4064b39b
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed May 7 09:40:20 2014 +1000

    Merge pull request #130 from serilog/feature-93-rollonlocked
    
    Roll-on-locked implementation for issue #93.

[33mcommit ecf141317dce85e2db5fc348e0120042b2d929f2[m
Author: Michiel van Oudheusden <m.vanoudheusden@gmail.com>
Date:   Sun May 4 15:30:25 2014 +0200

    Added counter and gauge to extras.timing assembly

[33mcommit 06a61a4b867413950258aff5eb8c5de158883514[m
Merge: d015d474 5ce3330e
Author: Michiel van Oudheusden <m.vanoudheusden@gmail.com>
Date:   Sun May 4 15:15:50 2014 +0200

    Merge branch 'master' into metrics

[33mcommit 5ce3330ea94ee607103f7641d329952b6c975164[m
Merge: de440e03 f733df2a
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun May 4 17:03:01 2014 +1000

    Merge pull request #131 from mivano/timing-add-property
    
    Added additional time in ms property to timing extension

[33mcommit 82030f435b5a7d0c1bbd5a9cc31ee78c1eafa3c0[m
Author: Michiel van Oudheusden <m.vanoudheusden@gmail.com>
Date:   Sun May 4 01:24:10 2014 +0200

    Added raygun.io sink

[33mcommit f733df2af9407c5f7282acbd05cb7ba4c9ba1f22[m
Author: Michiel van Oudheusden <m.vanoudheusden@gmail.com>
Date:   Sun May 4 00:10:58 2014 +0200

    Added additional time in ms property to timing extension so you can report on this value.

[33mcommit 4064b39b0f4ac1225d9345d7067ef8590734fdb4[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Apr 30 22:00:49 2014 +1000

    Initial roll-on-locked implementation for issue #93.

[33mcommit de440e03fc7fc629540f2b94bf970f64d6ddcb22[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Apr 28 20:18:46 2014 +1000

    Fixed the .NET 4.0 build.

[33mcommit 778caf5edd3b455d6acdc9d3b0a02619ae8a123a[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Apr 28 19:52:16 2014 +1000

    Mirrors the fix for https://github.com/continuousit/seq-releases/issues/118 to properly format decimal values in JSON regardless of culture.

[33mcommit 670058740617c1e16a955411827b873e8dce4021[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Apr 26 13:55:25 2014 +1000

    Minor tweaks to factoring/project structure.

[33mcommit 5a3e5189a0073aeaa77af4ea5ef5f97474586f1a[m
Merge: add626b4 873d3a48
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Apr 26 13:09:46 2014 +1000

    Merge branch 'master' of https://github.com/mderriey/serilog into mderriey-master

[33mcommit add626b4ec7e716ec0d133476758089b929dbac4[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Apr 26 12:54:14 2014 +1000

    Beefed up JsonFormatter: supports delimiting output, format providers, inclusion of rendered message. Fixes #107 and fixes #113.

[33mcommit 34473b771c1f9fd6afb9447c424ad4c70079c136[m
Merge: 40eddf14 b2424fdb
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Apr 25 08:14:35 2014 +1000

    Merge pull request #123 from Jaben/master
    
    MongoDb Sink Capped Collection Support (Issue #48)

[33mcommit 873d3a4847fdce23ab943460447832d290ded500[m
Author: Mickaël Derriey <mderriey@gmail.com>
Date:   Sat Apr 19 22:42:25 2014 +0200

    Second go at #108

[33mcommit b2424fdb811997862b6f0c969cb4a46f82103121[m
Author: Jaben <jaben@yetanotherforum.net>
Date:   Fri Apr 18 21:11:58 2014 -0400

    Another private slipped in there... naughty resharper.

[33mcommit 07e799fc14315b94572ae3c32bf09dc5fdd74d3b[m
Author: Jaben <jaben@yetanotherforum.net>
Date:   Fri Apr 18 21:09:55 2014 -0400

    No privates allowed.

[33mcommit 96c94fc650f94ebb1f89cb3c7736ad4ba86aabe7[m
Author: Jaben <jaben@yetanotherforum.net>
Date:   Fri Apr 18 21:05:34 2014 -0400

    That wasn't a great idea. Just added a MongoDbCapped extension that handles all the additional parameters for the capped collection. Also converted the Sink to just accept the IMongoCollectionOptions which gives some additional options for unique needs. Existing API is exactly the same.

[33mcommit d1a068730ebc03d1dfeb2502ff5286efee350d1b[m
Author: Jaben <jaben@yetanotherforum.net>
Date:   Fri Apr 18 20:21:17 2014 -0400

    Stab at issue #48.
    Not entirely sure how to handle backwards compatibility -- so I just added two optional parameters. But I changed the default to turn capped collection on and set to 100MB.

[33mcommit 40eddf14773e75fbe148a16b2ed7d65b596b588c[m
Merge: 8e528ada 67425dd0
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Apr 19 08:34:15 2014 +1000

    Merge pull request #119 from ShawInnes/master
    
    Updated LogEntries Cert

[33mcommit 8e528ada79392f58be27b96955012d34646ee594[m
Merge: 96d69297 dd2815fb
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Apr 19 07:07:45 2014 +1000

    Merge pull request #121 from Jaben/master
    
    Fix for issue #106

[33mcommit d015d474cc7e7a2de4d47af281882eb3ae9f64e3[m
Author: Michiel van Oudheusden <m.vanoudheusden@gmail.com>
Date:   Fri Apr 18 21:06:47 2014 +0200

    Improvements to meter

[33mcommit dd2815fbf6e94126992bda55cd6c38a97dd5175b[m
Author: Jaben <jaben@yetanotherforum.net>
Date:   Fri Apr 18 10:51:55 2014 -0400

    Resharper put the using in the namespace. Not consistant with the existing file.

[33mcommit d01185254d4a8f170956ff526dfe0a49fb2fe862[m
Author: Mickaël Derriey <mderriey@gmail.com>
Date:   Fri Apr 18 15:39:59 2014 +0200

    Attempt at providing alignment (see #108)

[33mcommit 67425dd0be11b3f978a3de5b08fdd144419038cb[m
Author: ShawInnes <shaw@immortal.net.au>
Date:   Fri Apr 18 19:54:20 2014 +1000

    Updated api certificate to newly issued one.
    
    Closes #117

[33mcommit 5bae8523f604525e9a3ce07477dc6bd45acb9a85[m
Author: Jaben <jaben@yetanotherforum.net>
Date:   Fri Apr 18 01:53:59 2014 -0400

    Fix for issue #106
    Added ConvertTypeTo() function which supports extended type conversions -- currently, only Uri and TimeSpan.

[33mcommit 96d6929754d89810180a0bd8ae154428972eb4cb[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Apr 18 09:06:38 2014 +1000

    Update README.md

[33mcommit 765216345971a4a1e0067a8a9f1dfd28e25e0497[m
Author: Michiel van Oudheusden <m.vanoudheusden@gmail.com>
Date:   Tue Apr 15 00:40:28 2014 +0200

    Refined the timing functionality

[33mcommit 430b1c1acbf7025ea8c37ad22ddd9a8a9ea1c36f[m
Merge: 16473ddb f2d638ba
Author: Michiel van Oudheusden <m.vanoudheusden@gmail.com>
Date:   Mon Apr 14 23:41:28 2014 +0200

    Merge branch 'master' of https://github.com/serilog/serilog into metrics

[33mcommit f2d638baa8ed47bf824ed784dfb7bd17353b6734[m
Merge: 60e8120a e3e84d86
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Apr 15 07:34:21 2014 +1000

    Merge pull request #110 from mivano/mssql
    
    MS SQL Server sink

[33mcommit 16473ddb023bc96391473bee88954018e1dec35f[m
Merge: 5c7f1688 60e8120a
Author: Michiel van Oudheusden <m.vanoudheusden@gmail.com>
Date:   Mon Apr 14 23:26:12 2014 +0200

    Merge branch 'master' of https://github.com/serilog/serilog into metrics

[33mcommit e3e84d868ef2cbe39d880e60555f3984fd15ce64[m
Author: Michiel van Oudheusden <m.vanoudheusden@gmail.com>
Date:   Mon Apr 14 21:46:55 2014 +0200

    Renamed the MSSQL sink to MSSqlServer sink

[33mcommit 5c7f16882b77bf9bac54be5e502f5e352d5029c7[m
Author: Michiel van Oudheusden <m.vanoudheusden@gmail.com>
Date:   Mon Apr 14 21:00:37 2014 +0200

    Added licenses

[33mcommit 5be4947d9fe6b75ad3c33cb8d44f5cfc280196e0[m
Merge: 9ab1be7a 60e8120a
Author: Michiel van Oudheusden <m.vanoudheusden@gmail.com>
Date:   Mon Apr 14 20:59:57 2014 +0200

    Merged from upstream

[33mcommit 60e8120a12abee4db942d2ff03b123341fab1fff[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Apr 14 20:32:57 2014 +1000

    Housekeeping, fixed package creation for OWIN.

[33mcommit bd44ff7221a078a037d944b62bcd39a8296ed191[m
Merge: a9e6acdc d042b14e
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Apr 14 07:06:21 2014 +1000

    Merge branch 'msowin' of https://github.com/damianh/serilog into damianh-msowin
    
    Conflicts:
            Serilog.sln

[33mcommit a9e6acdcc3e175863abc789869efbfc756c9705c[m
Merge: 9ffbf03a 817068d9
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Apr 14 06:40:33 2014 +1000

    Merge pull request #111 from ShawInnes/master
    
    Preliminary Couchbase Server support

[33mcommit d042b14e98d704868c2f646ac8eec4d8b0593ec3[m
Author: Damian Hickey <dhickey@gmail.com>
Date:   Sun Apr 13 22:05:43 2014 +0200

    Serilog.Extras.MSOwin: add nuspec

[33mcommit 59e5c4564abc664fd81c680c154b4651b6d4d7f5[m
Author: Damian Hickey <dhickey@gmail.com>
Date:   Sun Apr 13 22:02:41 2014 +0200

    Serilog.Extras.MSOwin: sign the assembly

[33mcommit bfbd06ebe07c8e2c39945f74bbe102795400137c[m
Author: Damian Hickey <dhickey@gmail.com>
Date:   Sun Apr 13 21:41:10 2014 +0200

    Serilog.Extras.MSOwin: compile fix (field name)

[33mcommit 6837bbb51d6e32e627376a04cf23d195f4d3c977[m
Author: Damian Hickey <dhickey@gmail.com>
Date:   Sun Apr 13 21:38:25 2014 +0200

    Some xml comments

[33mcommit bc913397d2162dbdf52e4568b928da5ca8ef9f46[m
Author: Damian Hickey <dhickey@gmail.com>
Date:   Sun Apr 13 21:36:58 2014 +0200

    Serilog.Extras.MSOwin: Allow specifying of the context property name.

[33mcommit 470f89b19b6b63ccc33c6f1e55ccd4fa02cf28b3[m
Author: Damian Hickey <dhickey@gmail.com>
Date:   Sun Apr 13 21:18:48 2014 +0200

    Serilog.Extras.MSOwin: Add a middleware that opens a nested context property to allow correlation of log messages from an owin pipeline.

[33mcommit 7b8d95d56d6adebc3ecc3c33ae61f998a9dcea89[m
Author: Damian Hickey <dhickey@gmail.com>
Date:   Sun Apr 13 20:47:29 2014 +0200

    New project Serilog.Extras.MSOwin. MS.Owin.Logging ILogFactory implementation that creates a Serilog ILogger + tests

[33mcommit 817068d9abd3e8dceeae8204a463c5359a158cfb[m
Author: Shaw Innes <shaw@immortal.net.au>
Date:   Sat Apr 12 11:56:40 2014 +1000

    Refactored Couchbase Sink to duplicate process used in RavenDB

[33mcommit 2c7f91895c2c95d73b862af5eda189be51f88264[m
Author: Shaw Innes <shaw@immortal.net.au>
Date:   Sat Apr 12 11:56:14 2014 +1000

    Add Couchbase sample to Demo project

[33mcommit 7f7679c1ede8e3340339d510bc38efb706b6d966[m
Author: Shaw Innes <shaw@immortal.net.au>
Date:   Sat Apr 12 11:55:46 2014 +1000

    Fixed duplicate entries in repositories.config

[33mcommit a71d3cb92e918c0f2e5ddc15529cedfa43574132[m
Merge: 25c52e45 9ffbf03a
Author: Michiel van Oudheusden <m.vanoudheusden@gmail.com>
Date:   Fri Apr 11 14:03:59 2014 +0200

    Merge branch 'master' of https://github.com/serilog/serilog into metrics

[33mcommit 4fd31285b826ef85b912f0763c0f2c02178c3c03[m
Author: Shaw Innes <shaw@immortal.net.au>
Date:   Fri Apr 11 19:27:56 2014 +1000

    Move payload into loop

[33mcommit d9ef7f2d31c70de2fdb5a6b408c1bbe84d9c1943[m
Author: Shaw Innes <shaw@immortal.net.au>
Date:   Fri Apr 11 19:25:54 2014 +1000

    Remove Test Project
    Refactor the Emit function

[33mcommit 7888cff945e92ff1ffd984622b91b3200aa48845[m
Author: Shaw Innes <shaw@immortal.net.au>
Date:   Fri Apr 11 15:29:01 2014 +1000

    Unit Tests

[33mcommit 0dc641cf1b75fbb7bb6856e48a3728f8123fe884[m
Author: Shaw Innes <shaw@immortal.net.au>
Date:   Fri Apr 11 14:58:39 2014 +1000

    Add Couchbase Sink

[33mcommit 9ab1be7ace79b3e6c2f1b0f2113752ab7cf6cc35[m
Merge: 05ce7c45 9ffbf03a
Author: Michiel van Oudheusden <m.vanoudheusden@gmail.com>
Date:   Fri Apr 11 00:49:23 2014 +0200

    Merged from upstream

[33mcommit 05ce7c458daaef8c2450a9425c8727923f019441[m
Author: Michiel van Oudheusden <m.vanoudheusden@gmail.com>
Date:   Fri Apr 11 00:43:52 2014 +0200

    Included xml properties

[33mcommit 9ffbf03a53a76f65c1551a430c21724887fd3435[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Apr 9 19:50:04 2014 +1000

    Reverted the signing change for Loggr, Loggly and Elmah - referenced assemblies aren't signed.

[33mcommit 263f35165892c074f3065c3049005471654d379b[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Apr 9 19:27:30 2014 +1000

    Fixed the assembly name of the new logentries sink (was .Loggly). Noticed a few projects referncing the key without signing turned on, updated those.

[33mcommit 7a6e199e1320b68f29ee5a1bcc46988329b80211[m
Merge: 1d9a637c f3a8c743
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Apr 9 18:34:54 2014 +1000

    Merge pull request #103 from mivano/logentries
    
    Added a logentries sink.

[33mcommit 0d25838648f458e29cfa4ae6bc6f2862925b96d2[m
Author: Michiel van Oudheusden <m.vanoudheusden@gmail.com>
Date:   Wed Apr 9 00:25:42 2014 +0200

    Added first framework for a MS SQL sink

[33mcommit 25c52e4582a3a356fea9353a4920d9c8f76735bc[m
Author: Michiel van Oudheusden <m.vanoudheusden@gmail.com>
Date:   Tue Apr 8 21:15:24 2014 +0200

    Added extra ms property so it can be used in stores

[33mcommit 8ba3d5cc432793dec848422dc3c1fc1cbd94e571[m
Author: Michiel van Oudheusden <m.vanoudheusden@gmail.com>
Date:   Tue Apr 8 00:33:37 2014 +0200

    Added some extensions to the timing extras; gauge, counter and meter. Work in progress still.

[33mcommit f3a8c7439660f5f1af6b19bc3293f0a7468938f6[m
Author: Michiel van Oudheusden <m.vanoudheusden@gmail.com>
Date:   Mon Apr 7 21:38:25 2014 +0200

    Added copyright license for LE Client class

[33mcommit 1d9a637c7cdfad1c1d579933fdde1a27b427b147[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Apr 7 19:51:13 2014 +1000

    Fixed templated path roller tests broken by the switch from relative to absolute paths.

[33mcommit c3c2201d451a9e081f3fb3f40d956e65590239c8[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Apr 7 19:07:37 2014 +1000

    Fixes #105 - correct handling of rolling log files without a full path specified.

[33mcommit b13b66633646d6ed65e94d724b6d0ed15f2e9181[m
Author: Michiel van Oudheusden <m.vanoudheusden@gmail.com>
Date:   Fri Apr 4 23:18:44 2014 +0200

    Uppercase to lowercase

[33mcommit e2152a429fe7dc315bdb627e386c29c863bf7445[m
Author: Michiel van Oudheusden <m.vanoudheusden@gmail.com>
Date:   Fri Apr 4 23:16:23 2014 +0200

    Added license header to LeClient class.

[33mcommit 4f234a6156a15e7fa96fda0d471f0a87ad03ba4a[m
Author: Michiel van Oudheusden <m.vanoudheusden@gmail.com>
Date:   Sun Mar 30 13:59:55 2014 +0200

    Added a logentries sink.

[33mcommit 42c57c32cf833396b48f13dc321fe69b93a739fe[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Mar 29 21:33:17 2014 +1000

    Fixes some issues with the app settings package (#3) that showed up with a bit more testing.

[33mcommit 54e86a01ae1c307a3556296d96b630b388ba108d[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Mar 29 21:23:31 2014 +1000

    Implements the basics of #3 - XML configuration via AppSettings.

[33mcommit 80db4a85319c1dea6e141934bceb40e8bf21f72e[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Mar 29 13:03:25 2014 +1000

    Fixed the assembly name for the Loggly sink, hopefully getting its NuSpec to pack.

[33mcommit ae156e1fcd7b3197bda8e2d56b7a5afcfe0343f1[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Mar 29 12:45:35 2014 +1000

    Fixed timing assembly name. (Tweaked let the operation ID flow through as a scalar, opp. for sinks with a native GUID type to preserve it.)

[33mcommit e540730d2168011f89b25e66caa7b381f977fd66[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Mar 29 12:04:14 2014 +1000

    Fixed file path to Serilog.Extras.Timing.dll in nuspec.

[33mcommit 23bd58397f4f8132d337d217fd92f99da3bbb014[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Mar 29 11:25:50 2014 +1000

    Use old-style <RestorePackages> settings to keep the TC build happy.

[33mcommit f534718d2027abbde5ce0c9465fef44be96410e1[m
Merge: cf9667b5 2dc15e65
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Mar 27 20:35:16 2014 +1000

    Merge pull request #102 from mivano/timers
    
    Added a timing extension as described in issue #20.

[33mcommit 2dc15e657a70683daff36a1acf883801ebacf15d[m
Author: Michiel van Oudheusden <m.vanoudheusden@aerdata.com>
Date:   Tue Mar 25 00:25:32 2014 +0100

    Added a timing extension as described in issue #20. For now it is a very basic implementation.

[33mcommit cf9667b5dbda003a0c7512e7289011e8351e6237[m
Merge: 585a0e37 d14b5278
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Mar 23 08:41:31 2014 +1000

    Merge pull request #100 from mivano/sesionspelling
    
    Fixed two small spelling mistakes in variables names.

[33mcommit 585a0e3712a5c9bd143b2eb7981c9ff6cf733eed[m
Merge: e12bf5d3 0da42aee
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Mar 23 08:41:03 2014 +1000

    Merge pull request #99 from mivano/usernameenricher
    
    Http username enricher

[33mcommit e12bf5d356245d8f384d0a7ec304f9779120f024[m
Merge: 13b605f1 ec72f1b9
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Mar 23 08:39:03 2014 +1000

    Merge pull request #98 from mivano/enrichers
    
    Added With* enrichers for machine name and process ID

[33mcommit 13b605f1da969db7d9da20447723b85530ac67ba[m
Merge: 5c9e41aa 7c36950c
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Mar 23 08:37:53 2014 +1000

    Merge pull request #97 from mivano/essinkdispose
    
    Elastic Search sink fixes

[33mcommit 5c9e41aa0691a670ff30d6a4423f105d060dc513[m
Merge: 30c9e41e 68cc35ef
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Mar 23 08:36:08 2014 +1000

    Merge pull request #101 from mivano/loggly
    
    Loggly.com sink

[33mcommit 68cc35efa58c85b0f74ad3cc6f790ce1af781996[m
Author: Michiel van Oudheusden <m.vanoudheusden@gmail.com>
Date:   Sat Mar 22 01:24:42 2014 +0100

    Created a loggly.com sink

[33mcommit d14b5278f6748e3cbc2fdace42defd2d553df4df[m
Author: Michiel van Oudheusden <m.vanoudheusden@aerdata.com>
Date:   Fri Mar 21 18:06:00 2014 +0100

    Fixed two small spelling mistakes in variables names.

[33mcommit 0da42aee05ad7964546b8bb4eabd47a8b4e556af[m
Author: Michiel van Oudheusden <m.vanoudheusden@aerdata.com>
Date:   Fri Mar 21 18:00:57 2014 +0100

    Added ability to specify how the username enricher should handle anonymous users.

[33mcommit fd2d228ebc926d3482c69325be37e4276a66bd59[m
Merge: 8759d27e 30c9e41e
Author: Michiel van Oudheusden <m.vanoudheusden@aerdata.com>
Date:   Fri Mar 21 17:42:50 2014 +0100

    Merge branch 'master' into usernameenricher

[33mcommit ec72f1b9d1d71d0b5bbd8111d851139fc1afdce3[m
Author: Michiel van Oudheusden <m.vanoudheusden@aerdata.com>
Date:   Fri Mar 21 16:26:01 2014 +0100

    Added With* enrichers

[33mcommit 7c36950c98febaeba1aa52f685d0947cca553852[m
Author: Michiel van Oudheusden <m.vanoudheusden@aerdata.com>
Date:   Fri Mar 21 16:08:58 2014 +0100

    Changed the gist with logstash template

[33mcommit bea1c7ca89256b2ff5668721e2abee4e9a250c99[m
Author: Michiel van Oudheusden <m.vanoudheusden@aerdata.com>
Date:   Wed Mar 19 13:52:51 2014 +0100

    Changed the way the ES sink sends its data to ES. It was not disposing correctly using the async method.

[33mcommit 30c9e41e571c2227aa4d63f41f19755c6ab3f9bd[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Mar 16 17:57:44 2014 +1000

    Fixed destructuring/JSON formatting of char and bool values. #96.

[33mcommit a461c9378acb9f5b8473992563c803e9a734702e[m
Merge: e5e1ce16 6aa3fbe5
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Mar 16 17:37:53 2014 +1000

    Merge branch 'master' of https://github.com/serilog/serilog

[33mcommit 6aa3fbe5e462fc41b79c0492792b26756140b537[m
Merge: 21b9c5b5 f22151e1
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Mar 16 17:24:01 2014 +1000

    Merge pull request #94 from mivano/ElasticSearch
    
    Elastic Search sink

[33mcommit f22151e155c1ca385761fd6c53738e36a3b015b1[m
Author: Michiel van Oudheusden <m.vanoudheusden@gmail.com>
Date:   Sat Mar 15 23:20:18 2014 +0100

    Fine tune sink and sample project

[33mcommit 97df1c3dc8a203c78e93c0cbbdfa0bc4e6f0d7b2[m
Author: Michiel van Oudheusden <m.vanoudheusden@gmail.com>
Date:   Sat Mar 15 00:09:30 2014 +0100

    Added ElasticSearch sink and sample project.

[33mcommit 252e69af75d9dd499da2c69b40adb4a659c4c951[m
Merge: 0f0b3c3d 21b9c5b5
Author: Michiel van Oudheusden <m.vanoudheusden@gmail.com>
Date:   Fri Mar 14 20:46:29 2014 +0100

    Merge https://github.com/serilog/serilog
    
    Conflicts:
            Serilog.sln

[33mcommit e5e1ce163be283381c66e374127f2b5e54b81390[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Mar 14 23:42:07 2014 +1000

    Suppress some missing documentation warnings in the Splunk sink.

[33mcommit a45eb95c2aec8907a73c0ad91ab1212a5c8ec48c[m
Merge: 027eba16 21b9c5b5
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Mar 14 23:17:09 2014 +1000

    Merge branch 'master' of https://github.com/serilog/serilog

[33mcommit 027eba16d4ab06a92a421195a67da91a8fc36e59[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Mar 14 23:16:55 2014 +1000

    Tiny doc fixes.

[33mcommit 21b9c5b5122ea1a279329234e5ad3fce753c4db6[m
Merge: 138176c0 068d1ac3
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Mar 14 23:06:38 2014 +1000

    Merge pull request #92 from mivano/loggr
    
    Loggr sink

[33mcommit 8759d27ec1e4daeb11336402d4ab9561cbb875ce[m
Author: Michiel van Oudheusden <m.vanoudheusden@gmail.com>
Date:   Fri Mar 14 00:53:53 2014 +0100

    Added a username enricher to return the User.Identity.Name in web applications

[33mcommit 0f0b3c3da2b685dc8882c2cd420dbd6c5fc20323[m
Author: Michiel van Oudheusden <m.vanoudheusden@gmail.com>
Date:   Mon Mar 10 23:51:54 2014 +0100

    Switched the demo project back to 4.5 instead of 4.5.1

[33mcommit 125b2aff2001508f83d57ad81a0516ddeffd9f9f[m
Author: Michiel van Oudheusden <m.vanoudheusden@gmail.com>
Date:   Mon Mar 10 23:41:14 2014 +0100

    Fine tune

[33mcommit 634080558c6086f2622af097131a3500f572ab93[m
Author: Michiel van Oudheusden <m.vanoudheusden@gmail.com>
Date:   Mon Mar 10 23:27:07 2014 +0100

    Added a sample project

[33mcommit 068d1ac3edd737183ec1ce2af2b8a1d1ed1f679d[m
Author: Michiel van Oudheusden <m.vanoudheusden@gmail.com>
Date:   Fri Mar 14 00:15:47 2014 +0100

    Cleanup

[33mcommit ad6910875774bf671734307439010e1d201346c7[m
Author: Michiel van Oudheusden <m.vanoudheusden@gmail.com>
Date:   Fri Mar 14 00:05:03 2014 +0100

    Updated comments

[33mcommit 451b4cb36df31de453ef09f311dd29b54beede9f[m
Author: Michiel van Oudheusden <m.vanoudheusden@gmail.com>
Date:   Fri Mar 14 00:01:05 2014 +0100

    Added loggr sink and demo project

[33mcommit 138176c00911ef5e76153f86b1777375a96fb194[m
Merge: 0b34abf6 62197376
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Mar 12 14:48:27 2014 +1000

    Merge pull request #90 from mivano/elmahio
    
    Elmah.io sink from @mivano

[33mcommit 62197376e2fb8a81b52751cac161d32dfbaf21fa[m
Author: Michiel van Oudheusden <m.vanoudheusden@gmail.com>
Date:   Mon Mar 10 23:51:54 2014 +0100

    Switched the demo project back to 4.5 instead of 4.5.1

[33mcommit 77c91a8f72390500a914a55cc3f79e878d2f601f[m
Author: Michiel van Oudheusden <m.vanoudheusden@gmail.com>
Date:   Mon Mar 10 23:41:14 2014 +0100

    Fine tune

[33mcommit e63e3e0d71ec0ed32cdf5c359f5d485560b57db4[m
Author: Michiel van Oudheusden <m.vanoudheusden@gmail.com>
Date:   Mon Mar 10 23:27:07 2014 +0100

    Added a sample project

[33mcommit 0b34abf6d3719a9361b1d9d0ca98de62dcb51fc5[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Feb 19 22:42:06 2014 +1000

    Removed WebHarness from the release build configuration so extra dependencies can be avoided on the build server.

[33mcommit eb3a68e67bac3634d62802dd15dc5561446e6dc2[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Feb 19 22:18:22 2014 +1000

    Upgraded solution to VS2013.

[33mcommit 66274751ae91959b4acf1320c52bbc59b2e714bc[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Feb 19 07:28:59 2014 +1000

    Couple of minor formatting updates.

[33mcommit e445f62a1f8c57be81c1a6788746cf7b3a8fd847[m
Author: Jeremy Clarke <jeremy@mellowfruit.com>
Date:   Sat Feb 15 18:51:58 2014 +0000

    Added EventLogSink and added lines to Demo.Program to show how to use it.

[33mcommit 462e8d2e728c343a70f67e8cc3138d9f944d44ef[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Feb 12 20:05:25 2014 +1000

    Fixes #84 - don't generate a large number of exceptions when the rolling file sink fails to open the target log file.

[33mcommit f10c7de56e49d6993db8abeab60782991c35d4de[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Jan 13 21:18:42 2014 +1000

    Fix the #75 fix.

[33mcommit 6cf1dfdbd61290d454622f630fded9e7c261ad3f[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Jan 13 21:15:29 2014 +1000

    #75 backport for .NET 4.0

[33mcommit c80fc05ef61704eea16885cf004fd80ed4e2ca45[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Jan 13 21:08:05 2014 +1000

    #75 - if depth limiting kicks in causing dictionary keys to be "cut off" from the serialized object graph, filter them out so that the dictionary construction doesn't fail.

[33mcommit 7ca4c543660903bf181baa7c798f2a2a60fd22cd[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Jan 10 21:35:54 2014 +1000

    Fixes #77 - empty the log context when crossing app domain boundaries.

[33mcommit 4614d5b415937a26ce148ed050ba985773aa04ca[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Dec 17 07:44:41 2013 +1000

    Fixes #75 - non-unique property names appear after destructuring in some circumstances. Struggling to find a way to do this using the 4.5 reflection APIs that doesn't incur the overhead of creating a hash-set or dictionary.

[33mcommit 455d6ac623b30313041b9691593c32a69afe73ed[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Dec 1 06:52:27 2013 +1000

    Updated the -net40 solution with the new formatting changes/constants.

[33mcommit f9de8f73ae281a4d49a4a838608aa2c2b2615ab2[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Dec 1 06:49:30 2013 +1000

    Fixed some build breaks introduced in the last commit.

[33mcommit 0ee30fdeffdd31880d8d69f26f68ff517ed85d8d[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Dec 1 06:42:46 2013 +1000

    Fixes #67 - Serilog.Core.Constants class to expose any values we treat specially (currently just "SourceContext").

[33mcommit b6a33d823fec9ef4f10e3f20aca2866cc0a9f32f[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Dec 1 06:36:14 2013 +1000

    Refactor to allow custom formatters to be applied to log events, specifically targeting JSON. Does away with a specific DumpFileSink in favour of a RawFormatter. Might help us get a first cut of #66 implemented.

[33mcommit 9f196f26cc5989ec47f86cc9601c34b7b6087be6[m
Merge: a12c4021 1b07b153
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Nov 24 22:43:09 2013 +1000

    Merge branch 'master' of https://github.com/serilog/serilog

[33mcommit a12c4021e56370f5954fb36c487e0f9aee17d1e2[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Nov 24 22:40:26 2013 +1000

    #75 part 2 - survive destructuring System.Type and System.MemberInfo (pass these as scalars); record an error tag rather than throwing if a property accessor throws during destructuring.

[33mcommit 32274f5b336ab47f582006b4e91a5212cf05c7b7[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Nov 24 22:34:54 2013 +1000

    #75, part 1 - when converting dictionary keys, watch out for unique objects that are no longer unique after type conversion.

[33mcommit 1b07b153e8842b12607461e27fba54fbe9d0e634[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Nov 24 22:20:43 2013 +1000

    We don't need the file license boilerplate on the landing page - including this on source files is sufficient.

[33mcommit 1525b8390b10bfce08d9664f9aa9e29e3efc4523[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Nov 24 08:26:54 2013 +1000

    Updated 4.0-specific code to match the destructuring changes introduced in 1.1.

[33mcommit e5d8d78e4b471b61445a217ef6d0549a28d6c0eb[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Nov 24 08:15:02 2013 +1000

    Reference the 4.0 version of Json.NET from the 4.0 test project.

[33mcommit 2beacf8915ce55e1506805310e730881b5a17d8f[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Nov 24 08:08:18 2013 +1000

    The packages.config file is shared between .NET 4.5 and .NET 4.0 targets; keeping the .NET 4.5 references :) Removing it from the 4.0 CSPROJ to reduce future confusion.

[33mcommit a41ba5fce776decaf9289f562c10127964fef647[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Nov 24 08:03:53 2013 +1000

    Fixed packaged assembly paths in the .NET 4.0 build

[33mcommit 44cebde9309638c428d8b0dccf1ba25fc5d1dee6[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Nov 24 07:48:22 2013 +1000

    Turn on XDOC generation for Splunk sink.

[33mcommit d0b3ad4e6ffbd7e289a1f27cb0f81cc3e26fd4f0[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Nov 24 07:37:55 2013 +1000

    Added a missing EndProject to the .sln file

[33mcommit f33ac9cdaff49162c927017fe3e760cf1e424eb5[m
Merge: ff46ca19 cade7adf
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Nov 24 07:22:55 2013 +1000

    Merge branch 'master' of https://github.com/serilog/serilog

[33mcommit ff46ca1994459398ffcfa05aac0f6adfafbdce94[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Nov 24 07:22:37 2013 +1000

    Include .PDBs in the core NUPKG so that we increase our chances of getting useful stack traces back in error reports.

[33mcommit cade7adf5ec2ff312e9c1812dedb17c733e1524f[m
Merge: 1d5d9f82 d586995c
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Nov 22 16:40:00 2013 -0800

    Merge pull request #74 from ctolkien/master
    
    Added ability to specify the Azure Table Storage Table name

[33mcommit d586995c5320721e57cf62f74c7c57bd43aae7f3[m
Author: Chad T <chad@tolkien.id.au>
Date:   Thu Nov 21 14:00:45 2013 +1100

    fixed a slight formatting screwup

[33mcommit 68f0cb6c2d1f28cb336c6476cbf237efc8d19c4e[m
Author: Chad Tolkien <chad@sodadigital.com.au>
Date:   Thu Nov 21 13:51:48 2013 +1100

    Can now set an azure table storage table name

[33mcommit 1d5d9f82532e65ba9dfe1317a93a4475f52d3240[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Nov 20 06:46:27 2013 +1000

    Housekeeping - updated a bunch of dependencies, for the sample projects and also: RavenDB, log4net, Topshelf, Azure (misc), Glimpse, MongoDB, SignalR.

[33mcommit 70a1b0d6d9cf9121b581251014daad5c30f13fe6[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Nov 19 22:32:12 2013 +1000

    Removed the JSON.NET package brought in with the last commit.

[33mcommit 2d2e38deb95ad24e68c22a53d9a25cb3e72a2cff[m
Merge: 125d12f9 b3c3a824
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Nov 19 22:27:54 2013 +1000

    Merge branch 'Splunk' of https://github.com/merbla/serilog into merbla-Splunk

[33mcommit 125d12f90875969d153aa253892b07f047de1aea[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Nov 19 22:25:23 2013 +1000

    Restored repositories.config.

[33mcommit 902f4432a55edb34539e4f9b9c7e9b8c781a64df[m
Merge: 6fcb39e9 203f31e3
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Nov 19 22:18:26 2013 +1000

    Merge branch 'scottmeyer-master'
    
    Conflicts:
            packages/jQuery.UI.Combined.1.8.20.1/jQuery.UI.Combined.1.8.20.1.nuspec
            packages/jQuery.Validation.1.9.0.1/jQuery.Validation.1.9.0.1.nuspec

[33mcommit 6fcb39e99abb865243748748ff2ef3390294e1f4[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Nov 19 22:17:01 2013 +1000

    Commit to attempt line ending normalisation.

[33mcommit 203f31e39894fb6f568aa2833ffa0114b4140c9f[m
Merge: b9c5e405 b801b46a
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Nov 19 22:14:23 2013 +1000

    Merge branch 'master' of https://github.com/scottmeyer/serilog into scottmeyer-master

[33mcommit b9c5e405bf5b9e3a9a5a68f14a82515b52687365[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Nov 19 21:50:42 2013 +1000

    Issue #73, be more conservative about logging byte arrays.

[33mcommit f9828aa91a391269d1357db67275e0dd28dc6387[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Nov 19 21:19:06 2013 +1000

    Add a couple of extra tags to the nuspec.

[33mcommit 63fc1c55987aca5182b8fac5b01a5c7aa8b43735[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Nov 19 20:05:09 2013 +1000

    Tidied up docs, copyrights, added key/signing to the email sink.

[33mcommit 64c57db4d3d57c814e4ba4019efb8a0928bffbe7[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Nov 19 20:03:54 2013 +1000

    Flush SelfLog after writing, when enabled.

[33mcommit b3c3a82439e0e4f735a1eea85cac08fe0c64130f[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Mon Nov 18 21:01:15 2013 +1000

    Added dependencies to nuspec

[33mcommit 4eee11ae242220b077c7100ff76bb2f900706b77[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Mon Nov 18 20:41:28 2013 +1000

    Added context to logging using Serilog properties for JSON

[33mcommit b801b46a43a35659dce91a310a61830a3e699521[m
Author: Scott Meyer <scott.meyer@firehost.com>
Date:   Tue Nov 12 09:02:42 2013 -0600

    update nuget.exe

[33mcommit dcf6d96b9835d7686c0987880eb56d11fc5ed0eb[m
Author: Scott Meyer <scott.meyer@firehost.com>
Date:   Mon Nov 11 16:56:07 2013 -0600

    remove packages directory and enable nuget restore

[33mcommit fc3444e19a13d843815154f6d4ecf76cf4beb9f2[m
Merge: 301143af 67379a9b
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Tue Nov 5 21:31:24 2013 +1000

    Merge remote-tracking branch 'upstream/master' into Splunk

[33mcommit 301143af131c46131c2352dcd37c74ee4af78207[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Tue Nov 5 21:17:10 2013 +1000

    Changed to use EmitBatch not EmitBatchAsync

[33mcommit 060edb038f69c51b45c418c1962ff10321d02e39[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Tue Nov 5 20:09:29 2013 +1000

    Updated to use attach vs submit

[33mcommit 67379a9bbe8d656e811fb647f3e7d825f3970c11[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Nov 1 11:10:31 2013 +1000

    Emit XML documentation from the email sink project.

[33mcommit 959c5597162b10ae6b2826436cd8be66ea953f0e[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Nov 1 11:08:25 2013 +1000

    Add the email sink project to the solution.

[33mcommit 5336273f73b91640b156495c7545c49fc1e311b7[m
Merge: c8774b6b c8fb5451
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Oct 31 17:59:29 2013 -0700

    Merge pull request #64 from andymac4182/emailsink
    
    Adding email sink

[33mcommit c8774b6b85660bfe188a2a2fe43637ddeeabe74c[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Nov 1 10:46:42 2013 +1000

    Issue #54 - support for .NET 4.0; note that public use of IReadOnlyDictionary in the 4.5 version makes this version binary-incompatible, however for consumption scenarios (as opposed to writing sinks) binary compat should still be fine.

[33mcommit 1dd109865948f9d6a0924c9457b76b9be06c3a19[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Sat Oct 19 06:57:04 2013 +1000

    Renamed file

[33mcommit f4e7270e7a042d351b76ba47b6844001157db7a6[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Fri Oct 18 19:22:07 2013 +1000

    Notes on further implementation and cleanup

[33mcommit 1357cae7f863aeb97c8deb7781efe8181d57789a[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Fri Oct 18 18:54:30 2013 +1000

    Moved files to correct location, also changed to use render

[33mcommit c8fb5451d4bea51c07e79fbb422926c1650401ed[m
Author: andymac4182 <andrew.mcclenaghan@gmail.com>
Date:   Fri Oct 18 11:51:56 2013 +1000

    Adding email sink

[33mcommit 52af0a0cc44f13330c70e68632bdc974ae720e6b[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Oct 17 22:11:11 2013 +1000

    Automatically append a trailing slash to the base address for CouchDB HTTP calls (Issue #60)

[33mcommit ec5219a06b0b160b214a17d3cb8b5f83eb887e3f[m
Merge: dc62650f 5eb8e109
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Oct 17 04:57:13 2013 -0700

    Merge pull request #58 from tvjames/patch-1
    
    Updated readme to include the licence of the project for quick reference.

[33mcommit dc62650f589e8979f65e0c01578d1373f053098c[m
Merge: 22652cdc e0ee289d
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Oct 17 04:55:02 2013 -0700

    Merge pull request #63 from CGijbels/tag-fix-for-glimpse-package
    
    Added glimpse tag and removed log4net tag

[33mcommit b3c2f31e9d1546094000fd29ba12a82d838a9319[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Thu Oct 17 20:57:20 2013 +1000

    Corrected location of sample

[33mcommit 08d35bec78b574142fa890c1ade673c87e861b73[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Thu Oct 17 20:52:28 2013 +1000

    Added Sample project and trivial wire up of sink
    
    Need to add awaitable and disposable to the sink
    Need to add formatted to ensure content in Splunk

[33mcommit e0ee289d08c62a662423973caed79a8717ddbc9d[m
Author: Christophe Gijbels <git@gijbels-it.be>
Date:   Thu Oct 17 11:46:04 2013 +0200

    Added glimpse tag and removed log4net tag

[33mcommit 22652cdc99b6b0f0b13b6713b1f67c107aca7e51[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Oct 14 20:38:21 2013 +1000

    Closes issue #59, NullReferenceException with empty template and spurious parameters.

[33mcommit 2e65dcdc2cc78b514297e9381d5a3294b11446fd[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Sat Oct 12 12:29:38 2013 +1000

    More wireup

[33mcommit 0da6ea6c664204bed3279630142df5971b2d8be4[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Sat Oct 12 12:27:04 2013 +1000

    Added scaffolding following other projects

[33mcommit 38905fcdfcf34fa411a607c10bb12a27e17a9b91[m
Author: Matthew Erbs <matthew.erbs@merbla.com>
Date:   Sat Oct 12 12:03:35 2013 +1000

    Added initial Splunk library

[33mcommit 414e0e235570671ff9e77832dff4f5ceb30364ed[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Oct 12 08:07:26 2013 +1000

    Removed the obsolete status notice.

[33mcommit 5eb8e1096e1e759c0bdf2db2f77a20191397fd1d[m
Author: Thomas James <thomasvjames@gmail.com>
Date:   Fri Oct 11 14:57:55 2013 +1000

    GitHub Apache 2 licence template

[33mcommit 5116adb0c0fec16322e78c969fc914e862c790a4[m
Author: Thomas James <thomasvjames@gmail.com>
Date:   Fri Oct 11 14:53:51 2013 +1000

    Include the licence terms for quick reference

[33mcommit 176f6af8fc2f949c4fcf5fa9bada23a92e22c3fe[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Sep 30 07:43:17 2013 +1000

    Closes issue #33 - make MessageTemplate constructor public.

[33mcommit d39839e2c8ef2dbe62517918bf0066b5cb962279[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Sep 30 07:42:46 2013 +1000

    Removed superfluous SignalR reference.

[33mcommit e27a4a31c820cf9ca0bc8679d92cc7a90f1c429e[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Sep 14 12:45:59 2013 +1000

    Fixes issue #53, unable to output events with properties named the same as built-in format properties.

[33mcommit 357ba47233d6f117d1cdb7b137661b8a56e2b24d[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Sep 12 21:50:08 2013 +1000

    Fixes issue #52 - unable to create a nonexistent directory for rolling files; issue also impacted directory creation for regular file sink, also fixed.

[33mcommit 34ae6366cd9fbef9970c08c4c03e26a8be936df3[m
Merge: b77f19e5 a5711071
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Sep 12 21:24:34 2013 +1000

    Merge branch 'srasch-sink/SignalR'
    
    Conflicts:
            packages/jQuery.2.0.3/jQuery.2.0.3.nuspec

[33mcommit b77f19e57a26150f792e77313cc6725726fdbe70[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Sep 12 21:22:58 2013 +1000

    Deal with some apparent lline ending issues in the jQuery nuspec.

[33mcommit a5711071128b24e5287227cbb128fa31304c3002[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Sep 12 21:19:40 2013 +1000

    Include NuGet package dependencies; minor formatting, project configuration and documentation updates.

[33mcommit 36090f4145b32c5b1b9c4e1d21386a40fa21d5ae[m
Merge: a783e683 557112ff
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Sep 12 21:03:11 2013 +1000

    Merge branch 'sink/SignalR' of https://github.com/srasch/serilog into srasch-sink/SignalR

[33mcommit a783e68383d76ff64d8eae55b39412f4d726a85b[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Sep 3 22:09:13 2013 +1000

    A simple ambient context stack, for the full .NET 4.5 framework.

[33mcommit 557112ffacded17ac28f197eae6f123f8e627174[m
Author: Steve Rasch <srasch78@gmail.com>
Date:   Thu Aug 29 00:50:40 2013 +0200

    move Sink.SignalR to src-folder

[33mcommit 169402d4c9b034c0d17f1627f83923f83c54bd80[m
Author: Steve Rasch <srasch78@gmail.com>
Date:   Thu Aug 29 00:46:48 2013 +0200

    add nuget spec file

[33mcommit da0a2df0becfbdcfafd6b81b3dbfff43ab3d78f0[m
Author: Steve Rasch <srasch78@gmail.com>
Date:   Thu Aug 29 00:40:40 2013 +0200

    add demo in WebHarness-project

[33mcommit b1c0a80f8180f854803dab7b065b43a828ab4a88[m
Author: Steve Rasch <srasch78@gmail.com>
Date:   Thu Aug 29 00:40:10 2013 +0200

    add implementation for SignalR (copycat from RavenDb :))

[33mcommit 162ba52896b3855b4b5c0a0ccbd577c5c085d521[m
Author: Steve Rasch <srasch78@gmail.com>
Date:   Wed Aug 28 09:48:59 2013 +0200

    add new solution for SignalR

[33mcommit e86bf7e04b892ee73305d93ec00e87deca164af2[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Aug 24 10:58:54 2013 +1000

    Warnings-as-errors and XML documentation in the Glimpse sink.

[33mcommit d267040a25279ae8a3e809714749ff380254df15[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Aug 24 10:51:00 2013 +1000

    Fixed/updated nuspecs.

[33mcommit dd298f941959595ba756adc927c44fe8cd1a0675[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Aug 24 10:42:23 2013 +1000

    Tweaked Glimpse sink formatting.

[33mcommit 97d7be83531bec8f7a47a6163a5b40ba2f3f7f96[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Aug 24 06:51:27 2013 +1000

    Show structured properties in Serilog Glimpse tab.

[33mcommit 62fd1f94b0be53f91268860c5d8358716574d095[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Aug 24 06:39:35 2013 +1000

    Moved Serilog.Web to Serilog.Extras.Web - following the long-term naming scheme we ended up with in Autofac.

[33mcommit 39183fd9a8a8c10127338741e5e1d10f54254327[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Aug 24 06:26:24 2013 +1000

    Included new Serilog.Extras.Topshelf; updated package dependencies and fixed some copyright headers/attributions.

[33mcommit 9cf19da7cde24dbaedbcab338d954fc03cf9a150[m
Merge: 8bef32cb 95edc98c
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Jul 30 13:31:57 2013 -0700

    Merge pull request #46 from droyad/GlimpseSink
    
    Added Glimpse Sink

[33mcommit 95edc98ced1e1c9ba8bea799c771dbb95ac625fd[m
Author: Robert Wagner <robert.wagner@readify.net>
Date:   Tue Jul 30 21:35:41 2013 +1000

    Added Glimpse Sink

[33mcommit 8bef32cb1a340ccd90cfa28fdc7ed1fe1ea1e9f6[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Jul 30 07:05:10 2013 +1000

    Added missing null check in Azure table storage sink

[33mcommit 54542210de2c43d2b7c1c6bbbb1b6c499998f520[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Jul 22 22:09:19 2013 +1000

    Switch allowing application lifecycle logging to be disabled in the web module.

[33mcommit a81238a450034604e4b5894b1ac817aa404edc01[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Jul 22 20:55:25 2013 +1000

    Tagline refresh.

[33mcommit fecd8e6f48ede570d45830e49c7809edd5adbe27[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Jul 22 20:49:36 2013 +1000

    Logo update, derived from "Sewing Needle" http://thenounproject.com/noun/sewing-needle/#icon-No3135 by Kenneth Appiah. Licensed under Creative Commons CC0.

[33mcommit ffa9c9347d9b9ad2df1008d614b8dfc8098872a0[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Jul 20 20:03:26 2013 +1000

    Fixes issue #45 - stack overflow serializing some cyclic graphs

[33mcommit 9267e6ca6e4605a43a771a7b193509c84bf5c4a6[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Jul 12 21:55:07 2013 +1000

    File retention limit for rolling file sink

[33mcommit 7836601cae23250d67605ae7f613cb612f074eb1[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Jul 11 18:06:31 2013 +1000

    WriteTo..... Observers! :)

[33mcommit 685c5172279c455c5b00c8ff8bf279c2f704d780[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Jul 10 22:28:01 2013 +1000

    Started improving the rolling file sink to enable retention policies; breaking change switches path format 'date' marker from {0} to {Date}.

[33mcommit fb4be429c28b76b815e9642f69f6d40722f8d43a[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Jul 9 22:45:14 2013 +1000

    Test to ensure enrichers execute in declared order.

[33mcommit 1770520f27309503fe635e03d62ee2171fe5ad9e[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Jul 9 22:25:17 2013 +1000

    Internal namespace reshuffle around core pipeline.

[33mcommit b289dbcde3e0f7366d90daf66e00c94f4cc58de3[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Jul 9 22:24:15 2013 +1000

    Removed obsolete web configuration extension method; added enricher based on HttpWorkerRequest.RequestTraceIdentifier (issue #44).

[33mcommit 88eed315e1e4aec0c6185b159ab4614006767560[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Jul 9 22:22:27 2013 +1000

    Removed some lingering temporary console apps

[33mcommit 477be61eb2026aa6b1ae8e4c403340a0a9c17ad6[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Jul 9 08:32:09 2013 +1000

    Immutability check over log event property values.

[33mcommit 4ddd1ee74db7241c6ecdc1a8fb82d5304629e210[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Jul 9 08:22:35 2013 +1000

    Disable logging of posted form data in web extension by default because of the difficulty in predicting what sensitive information might need to be filtered.

[33mcommit e1c51d1368b3cb46963df1ba79dc2e3589d0c964[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Jul 8 17:42:41 2013 +1000

    Default 1 GB file size limit for file logs

[33mcommit e85b73e1e3017c5d3eb8fc89fd4031ab82379a43[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Jul 6 22:11:09 2013 +1000

    Created HttpRequestNumber enricher for integer (friendly) request id, HttpRequestId now uses GUID to aid correlation. Minor breaking changes making property name constants consistent between enrichers.

[33mcommit 9fb9b0e3becb69f0ddc34912794fbe6f7226885d[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Jul 6 21:49:20 2013 +1000

    Include process id enricher

[33mcommit b7450e7c690474d188063a03492e7c7e81b6dcaa[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Jul 6 21:41:01 2013 +1000

    Split session and request ID properties to avoid recording superfluous information (e.g. with sessions disabled)

[33mcommit 911e0cafd083b5ac23d5cbe82e4e8f732b32473a[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Jul 6 21:00:37 2013 +1000

    A little more love for the web app integration

[33mcommit 357042d46f337d1291ad445357a468f23e54bbbc[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Jul 5 08:29:34 2013 +1000

    Attempt directory creation in file sinks.

[33mcommit ee6feebafa68a69f8a84005270890b271ec8623d[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Jul 5 08:24:53 2013 +1000

    API niceties - allow generic specification of type parameters to methods when only reflection-based overloads are available, and vice-versa.

[33mcommit acd6bbeb4965c955fb731ddb4d8e731b01bc6706[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Jul 5 08:14:43 2013 +1000

    Updated and made public the HttpProperties enricher

[33mcommit 1daad274c21ceeaa5bf49eb92d885b75bfd2f27c[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Jul 3 22:07:17 2013 +1000

    Closes issue #43 - early stringification of unrecognised scalar values; fixed badly written test revealed by this change.

[33mcommit f311989fb6692113c5050d47addd3f0639d87a53[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Jun 28 22:53:23 2013 +1000

    Matcher for events emitted under a dotted source namespace/type name path.

[33mcommit 0452296721ff6d4f07d3171709402e5535962258[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Jun 14 20:36:49 2013 +1000

    Fixed comment and minor tidy-up.

[33mcommit acd4adf0bcbda25f7e0889b4bd1ee7891f5c8779[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Jun 14 19:59:57 2013 +1000

    Collapse $typeTag on structures into the resulting JSON object for RavenDB

[33mcommit 96c9e2aa0c6180bed4cda114cc89e706609cae54[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Jun 14 07:32:08 2013 +1000

     - MachineName and ThreadId enrichers
    
     - log4net sample and fixes
     - trimming down of LogEvent.Properties to map name -> value rather than name -> (name, value)

[33mcommit 1838ee75d70549ecc64e4b31feadc8a89c586c71[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Jun 13 21:43:31 2013 +1000

    A first and basic cut at writing to log4net.

[33mcommit 0b135d73a68841b77752242358930532709157c4[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Jun 7 22:14:11 2013 +1000

    Speedup (> 2x) of message template property binding

[33mcommit 105a4d71169a59aab44e253bfacd77b531883ca7[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Jun 2 21:04:24 2013 +1000

    Show exception messages in highlight when writing to coloured console.

[33mcommit 8d16eca2a9a5b0bdcdaf53a80f34b01d484f1368[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Fri May 31 21:14:14 2013 +1000

    Simplifiy types on their way through the RavenDB sink (Issue #41).

[33mcommit 9d74066b6400e3590e88c3ea7683105f0430a7ab[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Fri May 31 07:06:24 2013 +1000

    Readme markdown.

[33mcommit e251e3fc6c1455d800b2014ac1fe028c0a1579ae[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Fri May 31 07:04:18 2013 +1000

    Updated readme.

[33mcommit 0d6f14683e210c00e8dfade28daa18172529a0bb[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Thu May 30 21:12:14 2013 +1000

    Getting to know the contributed RavenDB sink by tinkering with the test case.

[33mcommit 8af8933d757b1b173ec938bccdf4eb4b99cfb673[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Thu May 30 21:00:11 2013 +1000

    Make spelling of timestamp in RavenDB sink consistent with LogEvent.

[33mcommit c774d50f9e368574fb432ed33453746e8e865946[m
Merge: 6503e6e9 a22b0582
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Thu May 30 20:52:21 2013 +1000

    Merge branch 'master' of https://github.com/nickvane/serilog into nickvane-master

[33mcommit 6503e6e96e374d9c2822c82552c915c22f5eea6b[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Thu May 30 07:41:48 2013 +1000

    A first cut at some 'friendly' filtering.

[33mcommit a22b0582a671742cb6c64e3bb958bfcfbd71c885[m
Author: Nick Van Eeckhout <nick.vaneeckhout@gmail.com>
Date:   Tue May 28 22:58:30 2013 +0200

    Added RavenDB sink
    
    An implementation of a sink for storing logevents to RavenDB. It
    implements from PeriodicBatchingSink and uses RavenDB's async session.
    The current implementation is using a wrapper class around the logevent
    (like the azuretablestorage sink) to store as a document.

[33mcommit 72c9a06f98ced72b9c5e33e272b7352726c899da[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Mon May 27 22:02:52 2013 +1000

    Tags in NuSpecs.

[33mcommit 77920fb9389a990ff3db2e01f5ef736fe457bb8f[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sun May 26 21:21:52 2013 +1000

    Removed inaccurate comment.

[33mcommit dd369e7fa79a2087327cf02e92f3674a41c0b418[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sun May 26 11:13:43 2013 +1000

    Basic logger hierarchies: WriteTo.Logger().

[33mcommit 03aca1c07d5dd253ecc71fd86d5980dc634f914c[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sun May 26 10:00:12 2013 +1000

    Support for dictionaries as property values. SimpleJsonFormatter isn't so simple anymore :)

[33mcommit 4f8c3eeb1faeec4b7305e02f0eb0b7cabd4d1a5d[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sat May 25 21:57:09 2013 +1000

    Generate xdoc in release mode for Azure Table Storage sink

[33mcommit 5afa8c26007f510aa1ccab0f7e91afd65bbdf4fb[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sat May 25 21:53:18 2013 +1000

    Moved Azure sink to src directory

[33mcommit 4ee1e886cf49397bd720f41f06a760fabe418fda[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sat May 25 21:20:53 2013 +1000

    Dropped setting of format provider on base string writer, to maintain consistency with other parts of the code.

[33mcommit 6aa78d1a28b14b521a95b31508cbebad9b8e5b72[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sat May 25 21:16:53 2013 +1000

    Incorporated culture-specific formatting into MessageTemplateTextFormatter, rather than Logger and LogEvent.

[33mcommit 4ade81a016fe41f93ed679efea33f9aa47d59267[m
Merge: 369fbfbd a0ea801d
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sat May 25 20:19:19 2013 +1000

    Merge branch 'master' of https://github.com/felixg/serilog into felixg-master

[33mcommit 369fbfbd91e249510434c73109ccf15e529140db[m
Merge: cac46666 0328aee7
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu May 23 04:21:48 2013 -0700

    Merge pull request #34 from robdmoore/azure-table-storage
    
    [WIP] Added project for the Azure Table Storage sink

[33mcommit a0ea801d7e2b0fc94c4ed2be88d7a9c91732b43f[m
Author: Felix Gartsman <felix@yesumonim.co.il>
Date:   Tue May 21 11:21:09 2013 +0300

    Logger-wide IFormatProvider support.

[33mcommit 16621b1dcf467088e8a345131ef5ed0ddc6f7dc1[m
Author: Felix Gartsman <felix@yesumonim.co.il>
Date:   Mon May 20 18:30:17 2013 +0300

    Added support for culture-sensitive formatting.
    
    Allow to format log events using IFormatProvider.

[33mcommit 0328aee70aad72cfdd3a9ff88f4db030f7eb77ea[m
Author: Rob Moore <me@robdmoore.id.au>
Date:   Sun May 19 22:58:39 2013 +0800

    Moved out the LogEventEntity to a separate file

[33mcommit fb6807a4f1f48b68324b74ab695e195b4790f415[m
Author: Rob Moore <me@robdmoore.id.au>
Date:   Sun May 19 22:53:18 2013 +0800

    Added the initial implementation of the AzureTableStorage sink along with a bunch of todos

[33mcommit 25d092e8c81b82fcb93e536922acbfe377f60faf[m
Author: Rob Moore <me@robdmoore.id.au>
Date:   Sun May 19 22:30:16 2013 +0800

    Adjusting AssemblyInfo.cs for consistency with the other sinks

[33mcommit 9ee77a7ca79a213b59c45e3546c720e771994404[m
Author: Rob Moore <me@robdmoore.id.au>
Date:   Sun May 19 22:24:03 2013 +0800

    Added project for the Azure Table Storage sink

[33mcommit cac46666a90bf14cf6068df4f1bd6666fb25c8f9[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sun May 19 21:36:17 2013 +1000

    Removed release note post-release.

[33mcommit b5a754f3e222f81a889bf039d32048a724590c4d[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sun May 19 20:54:03 2013 +1000

    Release note about timestamp spelling.

[33mcommit 4b1b8f59951039c57b8e33913a32f6c548d0f3b7[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sun May 19 20:44:02 2013 +1000

    Timestamp is a single word - elminated camel hump. A source breaking change.

[33mcommit 6c82a9cdc34f0235c0ded730940f43f61ba5e9de[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sun May 19 20:23:27 2013 +1000

    Moved Minimum and Maximum from LogEventLevel to a new LevelAlias class, to avoid issues designating the intended level when converting with ToString(). Binary-breaking changes (unlikely to be source-breaking) - pre-release so bumping version to 0.3.

[33mcommit c2d09c26feeafc59bfa623a9c61b501fde150fd7[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Tue May 14 08:06:59 2013 +1000

    More complete/efficient JSON string escaping.

[33mcommit 35343d222620db4caaab4cb633d6c5ad8c471c4b[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Mon May 13 22:04:59 2013 +1000

    Fixed another JSON formatting bug - some time will be needed to implement escaping for all invalid JSON chars. Fixed broken self-log message.

[33mcommit 4bfc5f5c50ebd2653c5fc893538f7a75d19317b1[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Mon May 13 21:39:37 2013 +1000

    Less async; realised that we can't rely on timer disposal to ensure no work is active unless we consume the timer thread while waiting.

[33mcommit a51625b9957599395f15230f0407ff11c4e5e745[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Mon May 13 21:00:17 2013 +1000

    Partially lock-based concurrency in PeriodicBatchingSink.

[33mcommit 4b23a36ea54e693589f65d0ca151bc1d19f95564[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Mon May 13 20:09:25 2013 +1000

    Batching sink improvements - pump queue until empty; configurable timeout.

[33mcommit 2ac17281bf1be7e68452dd4b12ffeca13652723a[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Mon May 13 18:19:49 2013 +1000

    Use constant rather than -1 for infinite timer period.

[33mcommit 10316ff7ca234fe55da0d60b72e064a85a6884b3[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Mon May 13 18:18:28 2013 +1000

    Extracted batching from Couch and Mongo sinks to create a new PeriodicBatchingSink base class.

[33mcommit 11b691089f136fb33e29f0b4d62eee7c0d6af207[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sat May 11 07:21:00 2013 +1000

    Quick JSON escaping test.

[33mcommit 7ef7a2545b1c7afbad051fbb4d7ec1c3d424037d[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sat May 11 07:13:36 2013 +1000

    Another JSON escaping fix.

[33mcommit 37e0d6fd2c8d2580cc159ffabe2c832bf0f6070d[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Thu May 9 21:42:27 2013 +1000

    Pretty colored console appender.

[33mcommit 799d79d2e701a83eef3de3a6145af17965f78c0b[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Thu May 9 19:58:14 2013 +1000

    Started on colored console sink, improving the default format strings in the process.

[33mcommit ad6daa2fdcd85a47ffb69e19e85beaedf8430c83[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Thu May 9 08:29:23 2013 +1000

    Updated mongocsharpdriver and removed explicit version from mongo nupkg.

[33mcommit b30657b174a58a3599e2fc5b0cf4e0450c8ead51[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Wed May 8 07:55:53 2013 +1000

    Allow the batch size on Couch and Mongo sinks to be configured.

[33mcommit a39855925f24cbf7fb41c191f270ada2c97ba233[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Wed May 8 06:09:36 2013 +1000

    Escape rendered message in JSON-based sinks.

[33mcommit 57e5db9d23ea863c252dc003fdf9df6027015714[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Apr 30 08:29:28 2013 +1000

    Fixed JSON formatting issue affecting Couch and Mongo sinks.

[33mcommit 34d4fae925bed640115b04b2eba3af8c04f7e593[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Apr 30 08:16:43 2013 +1000

    Text property on TextToken

[33mcommit 0d0d268134c6e4b058449374d1b2e542f095d94a[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Apr 23 20:53:29 2013 +1000

    Issue #29, non-generic ForContext().

[33mcommit 6e88d3bdee63f4ca0bc2e461f14f50291ce21ec3[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Apr 8 21:20:54 2013 +1000

    Ensure Serilog.dll is included in net45 projects

[33mcommit bfa0a5619aac89ef9154d63cc72a3e980497f1cc[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Apr 8 20:45:08 2013 +1000

    Normalised line endings

[33mcommit 0a340358479489c866fbe489d7487ef181a64b1b[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Apr 8 20:41:00 2013 +1000

    Added gitattributes to set CRLF to auto.

[33mcommit ce5b3398f5577fc64c983a31121da0e5c90ad3d0[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Apr 8 20:23:45 2013 +1000

    Removed awkward namespace clashes; added simple in memory sink to enable phone/Win8 testing.

[33mcommit ef6d092058858939ac2ad672d590b96e698f8170[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Apr 8 17:59:47 2013 +1000

    PLIB support in NuSpec.

[33mcommit 18f6bfbf226a9e1b3f2782f76272e3e47311ec12[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Apr 8 17:56:20 2013 +1000

    Core ported to Win8/WinPhone8 portable subset.

[33mcommit 92d6091d57f38dac7d9c9247d60d36cccdbb82b8[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Apr 8 17:40:26 2013 +1000

    Moved trace sink.

[33mcommit 1c1aaad93b89595a966a03e2d40a08254ab68331[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Apr 7 20:33:31 2013 +1000

    Split full-fx types out into their own assembly.

[33mcommit 8fb6e5ff80d35779f4eac3cf751dca370b3332cd[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Apr 7 07:57:07 2013 +1000

    Major configuration syntax revamp to improve discoverability.

[33mcommit 649a755e5be0a134c3aaa4319010cf0076eb18b2[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Apr 6 13:30:18 2013 +1000

    Moved message template to Events namespace.

[33mcommit 952288a4d45e054a2b8dfe2a246d3ae76f2541c0[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Apr 6 13:02:31 2013 +1000

    Message when maximum destructuring depth reached.

[33mcommit aee15dc096f8fabc3438632fb7c29563b6f31549[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Apr 6 11:30:57 2013 +1000

    Limit the depth of destructuring to 10 levels of nesting.

[33mcommit b33ece0033dd78b6bfdec12a34c2e0337697b46b[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Apr 6 10:41:17 2013 +1000

    Simplified destructuring policies.

[33mcommit 4958bb4bb00b36ab0537f5e493cde979321683ed[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Apr 4 20:37:24 2013 +1000

    Transformations on values during destructuring. Closes issue #15. Still requires some thought around naming and API organisation, will do that in one pass soon.

[33mcommit 05998cd06db0f2d70aef9579a5cc9906958f8227[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Apr 4 20:10:24 2013 +1000

    Simple tests for nullables.

[33mcommit 04cfde8d4b63698f112ed2d5399e411ff17c3f4c[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Apr 4 07:53:19 2013 +1000

    Destructuring policies, nullables.

[33mcommit 9cfc9c8aacafc81b092bb962e8df1ee2a647df9e[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Apr 3 21:26:48 2013 +1000

    Allow types to be configured as scalars.

[33mcommit 7cca4a96b2d6be95d3f319919b2d89ac7bb8fbe7[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Apr 3 21:01:16 2013 +1000

    A byte[] is a scalar value.

[33mcommit 2dd232bb87fa6ecc637fd69f6372dee34899feed[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Apr 3 20:42:40 2013 +1000

    More refactoring towards configurable property conversion.

[33mcommit 0fe81a0839150e371c1d4707bbd4c7844d749dcd[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Apr 2 21:24:20 2013 +1000

    Refactored to add MessageTemplate (strongly-typed) and RenderedMessage to LogEvent, breaking the near-universal dependency on IMessageTemplateParser. Refactoring towards centralisation of the message template processing pipeline in MessageTemplateProcessor. Some renames/reorganisation on the horizon.

[33mcommit c8e6dc5512b23315aac471071a2cb5790fa0bbbd[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Apr 1 21:24:51 2013 +1000

    Issue #16 - Guids and Uris are always literals.

[33mcommit df70fa4eb81dc66f5d569a6f7eff30ccbac68be0[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Apr 1 20:09:41 2013 +1000

    Pointed readme to the project site.

[33mcommit 15060ab109f9b94304bad1166f645907c2cb0c52[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Apr 1 20:08:55 2013 +1000

    Removed old docs.

[33mcommit d79c5cd05219565f2f28bccdabb1092e7c656703[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Mar 30 17:54:36 2013 +1000

    Closes issue #13 - filtering.

[33mcommit 6b3ea5e4de4f05bb7c60f12c395822f679aabc78[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Mar 30 14:26:50 2013 +1000

    Closed issue #12, safety backstop when excess message templates are generated.

[33mcommit 5cfa01a96bf46ddf8dfda775a2eff3feafe25094[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Mar 30 14:21:45 2013 +1000

    Fixed a class name and default namespace in MongoDB assembly.

[33mcommit 39bfa9648c008f135ead0f0c65a3d8e8281357ce[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Mar 30 14:20:29 2013 +1000

    A couple of minor tweaks on top of the new MongoDB integration. Using _typeTag as the "meta" property name since it should be compatible with Mongo and Couch.

[33mcommit 9d6148d4b36dbdfbf004976ad136452c2942bafe[m
Merge: c4f3c96a 762b72a0
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Mar 29 21:12:13 2013 -0700

    Merge pull request #11 from chandmk/master
    
    Added MongoDb Sink from Kiran Makkapati.

[33mcommit 762b72a0ec60f94cdbc6f070c031aa953c01216d[m
Author: Kiran Makkapati <chandmk@gmail.com>
Date:   Fri Mar 29 10:05:13 2013 -0400

    adjusted the project structure

[33mcommit f7091472967fe26f644d3e1889a36dbf03fb8753[m
Author: Kiran Makkapati <chandmk@gmail.com>
Date:   Fri Mar 29 09:59:57 2013 -0400

    enabled nuget package management

[33mcommit 7cf664f14ce7c7874024c3900dba8cb0919d88c4[m
Author: Kiran Makkapati <chandmk@gmail.com>
Date:   Fri Mar 29 09:57:43 2013 -0400

    added nuspec

[33mcommit 182749f4c43c6b5a2872c6d0690dd1ce08303f52[m
Author: Kiran Makkapati <chandmk@gmail.com>
Date:   Fri Mar 29 09:57:29 2013 -0400

    added nuspec

[33mcommit 41f138a0e8aed6a38d56fef1fac23d256700ffab[m
Author: Kiran Makkapati <chandmk@gmail.com>
Date:   Fri Mar 29 09:44:27 2013 -0400

    added mongodb sink

[33mcommit c4f3c96a5acbf6ecb44c99ac6bfb3fd1e43c4abd[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Mar 27 20:35:57 2013 +1000

    Fixed the sample output template.

[33mcommit 2eab38209f85a06762472e7949532c461ab60c5d[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Mar 27 20:22:37 2013 +1000

    Completed XDOC.

[33mcommit c4d68b976ebcc8438b99edeebf036e5e9425ce00[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Mar 27 17:44:17 2013 +1000

    More XDOC.

[33mcommit de54b04d18332535cc0ff327cb868bdf9114afa8[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Mar 27 17:38:33 2013 +1000

    Made values IFormattable.

[33mcommit 877a1b0c1c017978a37a7a17f91b2e0261d75ddb[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Mar 27 08:14:30 2013 +1000

    API cleanup; almost done with XDOC.

[33mcommit ef78eb659fba8baf3c6d7f740ba877cba20aab06[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Mar 26 21:47:54 2013 +1000

    Signature improvements for ILogger.ForContext().

[33mcommit 3ddb232cb74875e80ae1c6c3723174ed7c62b3ce[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Mar 26 21:26:24 2013 +1000

    Very simple rolling file sink.

[33mcommit ce8c76e33e9d5927632bf9addd20128660663711[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Mar 24 22:11:55 2013 +1000

    Extra doc/sample.

[33mcommit 288904e777e041d487d8b3772a914799d84b7a3c[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Mar 24 21:48:45 2013 +1000

    Simple file sink.

[33mcommit 66c4bd6b781b775aed034a24c65d874c31caefad[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Mar 24 21:17:48 2013 +1000

    Allow colons in format strings.

[33mcommit 4bd3fd021fe2f0ce131b3dd6ea7169d0c6cf02ab[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Mar 24 12:05:32 2013 +1000

    Dispose sinks along with loggers.

[33mcommit 52f05481b0c176449f5146e1d568aa8c81986153[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sun Mar 24 10:06:20 2013 +1000

    Positional property handling.

[33mcommit c4b65ebfee69f278fcba04c0fcbd26b9e8175f7d[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Mar 23 21:36:01 2013 +1000

    Package icons.

[33mcommit d583478ad0fb361d0c7b75cb9bb1522bee79ec8f[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Mar 23 20:49:34 2013 +1000

    XDOC for Log/ILogger.

[33mcommit ad9e05379617d87fe974b474ed44cb71de05e132[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Mar 23 15:16:53 2013 +1000

    Fixed package id.

[33mcommit edf6b0846d6b9d7c04b29c5cd8e5b0cc3efd4efb[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Mar 23 14:18:09 2013 +1000

    Started commenting API surface.

[33mcommit 2ca86c985958feaa33a6f660af5c3caea6eae302[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Mar 23 13:53:18 2013 +1000

    Added missing file.

[33mcommit a9d071741cc0436872710ce5a22f91f13f8c1868[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Mar 23 13:47:50 2013 +1000

    NuGet related files.

[33mcommit c8e7c33557380bf2f81dbd7047cd288a6c303185[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Sat Mar 23 13:12:56 2013 +1000

    Nuspecs.

[33mcommit 00b8e329b8afb51c6491f444b4f0de4d5bbe26a2[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Fri Mar 22 21:01:14 2013 +1000

    A simple CouchDB appender and demo to accompany introductory article.

[33mcommit 4df51c5ac86679bbe011bf85dc625c243299852e[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Mar 21 21:36:55 2013 +1000

    SimpleJsonFormatter improvements.

[33mcommit 4ec1c1083da103a140bcc0fde6a90986034190f4[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Mar 20 22:01:59 2013 +1000

    Some simple JSON support (WIP).

[33mcommit 5cb5e7c91e3714472d7e37359f98cbfecd8f9a2f[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Mar 19 11:52:30 2013 +1000

    Removed HTTP sink.

[33mcommit 1a1d98c9a57e951f963e6d8e1a8b16f9463376ef[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Mar 18 07:24:13 2013 +1000

    Test for context property overriding; Write(LE) on ILogger.

[33mcommit 7cc42a7cd027d7c0a1645078de7799dde6a7d2fe[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Mar 6 20:30:30 2013 +1000

    Demo enhancement.

[33mcommit 8c2eb13504c5094f510113b224bfb0d440f49a22[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Mar 6 20:26:34 2013 +1000

    Factored out display formatting.

[33mcommit 878e8c65f1c4b8372009209d41ec548ec7bda8ad[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Mar 5 21:41:50 2013 +1000

    Show exception in logs.

[33mcommit 0728efb1c4abf6d45f2a84bef77b54dcb6028d01[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Tue Mar 5 20:30:45 2013 +1000

    ForContext<TSource> and tests.

[33mcommit 128dc33074f95ec38e22c6751bae6dec3b2d9355[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Mar 4 22:55:17 2013 +1000

    Use prefixes rather than format strings to control destructuring.

[33mcommit f65186d929ada797c5b2cb7da8324444449385a3[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Mar 4 22:34:37 2013 +1000

    Converted to use literal values in log event properties.

[33mcommit 562b8db51c6643748e5fc71d342b0186951e60b4[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Mar 4 21:48:06 2013 +1000

    Doc fix.

[33mcommit 9a253b18ee5b147ff7bf91779d02651ab6ffafc6[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Feb 28 22:14:48 2013 +1000

    Quick and dirty Web API server sink.

[33mcommit e5fe6b458f8a2d1579e2fe6ef41f6e3bf068b71b[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Feb 27 22:00:49 2013 +1000

    Safe enricher test.

[33mcommit 4883a7900145cb37b94c6fe173321a228b7d1d6e[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Feb 27 21:53:04 2013 +1000

    Exception-safe for sinks and enrichers.

[33mcommit 7d8f009eefc5caab164df557759a93b525c80d91[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Feb 27 21:00:04 2013 +1000

    Some basic web lifecycle logging.

[33mcommit 81a7d457b239f3df9befb2202ae8205e5648650b[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Feb 27 08:05:11 2013 +1000

    Web sample skeleton; trace sink.

[33mcommit a80090c02f2b021fdcecdd18e0b11b23e8c76fbd[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Feb 25 21:38:10 2013 +1000

    Default/Stringify/Destructure options.

[33mcommit 8f2a454c300331a8bae627989d3254c1a054547d[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Feb 20 21:49:39 2013 +1000

    Web enricher.

[33mcommit f3c1c095aee587525dcf75e3d30494e0f9a70978[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Wed Feb 20 20:15:45 2013 +1000

    Namespace rename.

[33mcommit 9dc71cd3d52d1138fdaa36393a9e83bb40e46de8[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Feb 18 08:05:05 2013 +1000

    Tidied up some APIs on LogEvent.

[33mcommit 93150046272c9c586e9a76aa8052853a8497f7a9[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Mon Feb 18 08:00:36 2013 +1000

    Parser tests and fixes.

[33mcommit 4f04524e70c47160cdaddcdeb0ef1b30c4e62317[m
Author: nblumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Feb 14 21:48:29 2013 +1000

    Moved to GitHub.

[33mcommit 5bb93d5625beb18d5b5046ff0f9443fefd514d3f[m
Author: Nicholas Blumhardt <nblumhardt@nblumhardt.com>
Date:   Thu Feb 14 03:44:04 2013 -0800

    Initial commit
