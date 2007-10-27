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
$dir = "/homeserver/pgsql/backup";
my @databases = ('regnskab3060', 
				'puls3060',
				'dbHafsjoldData', 
				'bacula', 
				'gallery2');

foreach $db (@databases) {
	$mask = $db . "_##.out";
	$dumpfile = GetOldestFileName($dir, $mask, 7);
	system "/usr/local/bin/pg_dump -b -F c -f $dumpfile $db";
}

