param($installPath, $toolsPath, $package, $project)

$DTE.ItemOperations.Navigate("http://babaganoush.falafel.com/download/complete?install=" + $package.Id + "=" + $package.Version)