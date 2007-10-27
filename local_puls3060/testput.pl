#!/usr/bin/perl -w
use DirHandle;
my $error = "";
my $newfiles = 0;

## sub bINBOUND
sub bINBOUND {
    $d = new DirHandle "/home/lkp/INBOUND";
    my $filecount;
    if (defined $d) {
        while (defined($_ = $d->read)) { $filecount++; }
        undef $d;
    }
    return $filecount - 2;
}

## Main
$newfiles = bINBOUND();
if ($newfiles) {
    $error = `sftp -b/home/lkp/putscript LKP\@194.239.133.178 2>&1 1>/dev/null`;
	print "Der er sendt $newfiles file(r)\n";
}
##print "--->\n$error<---\n";

