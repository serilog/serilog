for path in src/*/project.json; do
  dirname="$(dirname "${path}")"
  dnu restore ${dirname}
  dnu build ${dirname} --framework dotnet5.4 --configuration Release --out .\artifacts\testbin;
  dnu pack ${dirname} --framework dotnet5.4 --configuration Release --out .\artifacts\packages;
done

for path in test/*/project.json; do
  dirname="$(dirname "${path}")"
  dnu restore ${dirname}
  dnu build ${dirname} --framework dotnet5.4 --configuration Release --out .\artifacts\testbin;
  dnx -p ${dirname} test;
done

