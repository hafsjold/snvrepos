#!/usr/bin/perl -w
use DirHandle;
my $error = "";
my $newfiles = 0;

## sub bOUTBOUND
sub bOUTBOUND {
    $d = new DirHandle "/home/lkp/OUTBOUND";
    my $filecount;
    if (defined $d) {
        while (defined($_ = $d->read)) { $filecount++; }
        undef $d;
    }
    return $filecount - 2;
}

## Main
$newfiles = -bOUTBOUND();
$error = `sftp -b/home/lkp/getscript LKP\@194.239.133.178 2>&1 1>/dev/null`;
$newfiles += bOUTBOUND();
if ($newfiles) {
	print "Der er modtaget $newfiles file(r)\n";
}
##print "--->\n$error<---\n";

