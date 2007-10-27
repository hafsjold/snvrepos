#!/usr/bin/perl -w
use File::stat;

sub GetOldestFileName {
    my $dir = shift;	
    my $mask = shift;	
	my $i_max = shift; 
	my $f_mtime = 9999999999;
	my $f_name;
	my $i;
	
	for ($i = 1; $i <= $i_max; $i++){
		$fm = $mask;
		$fm =~ s/##/$i/;
		$fn = "$dir/$fm";
        if (-e $fn) {
			$fn_mtime = stat($fn)->mtime;
			if ($fn_mtime < $f_mtime) {
				$f_name = $fn ;
				$f_mtime = $fn_mtime;
			}
		}else{
			$f_name = $fn ;
			last;
		}		
	} 
    return $f_name;
}

## Main
my $dir = "/backup/dump";
my @file_systems = ('/', 
					'/usr',
					'/var', 
					'/data/home', 
					'/data/pgsql');

foreach $fs (@file_systems) {
	if ($fs eq "/") { $mask = "fs_root_##.dmp";}
	else{ $mask = "fs" . $fs . "_##.dmp";	$mask =~ s/\//_/g;}
	$dumpfile = GetOldestFileName($dir, $mask, 7);
	system "/sbin/dump -0aLua -C 32 -f $dumpfile $fs\n"
}

