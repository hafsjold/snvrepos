#!/usr/bin/perl -w
use File::stat;
use DirHandle;

my $SourceDir = '/backup/dump/';
my $SourceFile ='';
my $SourceFileTime =0;
my $TargetDir = '/homeserver/dump/';
my $TargetFile = '';
my $TargetFileTime = 0;

$d = new DirHandle $SourceDir;
if (defined $d) {
	while (defined($_ = $d->read)) {
		if ($_ eq '.') {next;}
		if ($_ eq '..') {next;}
		
		$SourceFile = "$SourceDir$_";
		$SourceFileTime = 0;
		if (-e $SourceFile) {
			$SourceFileTime = stat($SourceFile)->mtime;
		}
		
		$TargetFile = "$TargetDir$_";
		$TargetFileTime = 0;
		if (-e $TargetFile) {		
			$TargetFileTime = stat($TargetFile)->mtime;
		}
		
		if ($SourceFileTime > ($TargetFileTime + 1)) {
			print "cp -fpv $SourceFile $TargetFile - $SourceFileTime $TargetFileTime\n"; 
			##system "cp -fpv $SourceFile $TargetFile"
		}
	}
	undef $d;
}
exit;
