#!/usr/bin/perl -w
use Fcntl;
my $pbsfilesid = 853;
my $filename = "D1234567";
my $buffer = "dette er en testfile $pbsfilesid";

eval { Log_data_fra_pbc( $filename, $buffer, $pbsfilesid ) };

##################################################
# Log_data_fra_pbc in /home/lkp/OUTBOUND
##################################################
sub Log_data_fra_pbc {
   my $filename = shift;
   my $buffer = shift;
   my $pbsfilesid = shift;
   
   my $path = "/home/lkp/OUTBOUND";
   my $sinkpath = $path . "/" . $pbsfilesid . "-" . $filename; 
   
   sysopen( SINK, $sinkpath, O_WRONLY|O_TRUNC|O_CREAT, 0400) or die "Couldn´t open $sinkpath for writing: $!\n";
   syswrite( SINK, $buffer, length($buffer) ) or die "Couldn´t write to $sinkpath: $!\n";
   close SINK;
}
