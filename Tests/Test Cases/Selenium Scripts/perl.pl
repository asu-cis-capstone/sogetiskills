use strict;
use warnings;
use Time::HiRes qw(sleep);
use Test::WWW::Selenium;
use Test::More "no_plan";
use Test::Exception;

my $sel = Test::WWW::Selenium->new( host => "localhost", 
                                    port => 4444, 
                                    browser => "*chrome", 
                                    browser_url => "https://dev-sogetiskills.azurewebsites.net/" );

$sel->select_window_ok("null");
# $sel->fill_email_ok("id=EmailAddress");
# $sel->fill_password_ok("id=Password");
$sel->type_ok("id=EmailAddress", "");
$sel->type_ok("id=Password", "");
$sel->click_ok("css=input.btn.btn-primary");
$sel->wait_for_page_to_load_ok("8888");
$sel->select_window_ok("null");
# $sel->fill_email_ok("id=EmailAddress");
$sel->click_ok("css=input.btn.btn-primary");
$sel->wait_for_page_to_load_ok("8888");
$sel->type_ok("id=EmailAddress", "");
$sel->type_ok("id=Password", "");
# $sel->fill_password_ok("id=Password");
$sel->click_ok("css=input.btn.btn-primary");
$sel->wait_for_page_to_load_ok("8888");
# $sel->fill_email_ok("id=EmailAddress");
# $sel->fill_password_ok("id=Password");
$sel->click_ok("css=input.btn.btn-primary");
$sel->wait_for_page_to_load_ok("8888");
