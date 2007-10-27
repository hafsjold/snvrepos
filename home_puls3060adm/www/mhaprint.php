<?php

$pdf = pdf_new();

pdf_set_parameter($pdf, "compatibility", "1.3");

pdf_open_file($pdf);

pdf_set_info($pdf, "Subject", "Test af PDF-print");
pdf_set_info($pdf, "Title", "Puls 3060 Bestilling af Klubtøj");
pdf_set_info($pdf, "Creator", "Puls 3060");
pdf_set_info($pdf, "Author", "Mogens Hafsjold");

$image = pdf_open_image_file($pdf, "tiff", "puls3060lillelogo.tif", "", 0);

$template = pdf_begin_template($pdf, 595, 842);
  pdf_place_image($pdf, $image, 440, 740, 1.0);
  pdf_set_font($pdf, "Times-Roman", 12, "host");
  pdf_set_value($pdf, "textrendering", 0);
  pdf_show_xy($pdf, "Template text", 10, 10);
pdf_end_template($pdf);

pdf_begin_page($pdf, 595, 842);
  pdf_place_image($pdf, $template, 0, 0, 1.0);
  pdf_set_font($pdf, "Times-Roman", 24, "host");
  pdf_set_value($pdf, "textrendering", 0);
  pdf_show_xy($pdf, "A PDF document created in memory!", 50, 500);
pdf_end_page($pdf);

pdf_begin_page($pdf, 595, 842);
  pdf_place_image($pdf, $template, 0, 0, 1.0);
  pdf_set_font($pdf, "Times-Roman", 30, "host");
  pdf_set_value($pdf, "textrendering", 0);
  pdf_show_boxed($pdf, "A PDF document created in memory by Mogens Hafsjold!", 50, 500, 300, 6*30, "left");
pdf_end_page($pdf);

pdf_close_image($pdf, $image);
pdf_close_image($pdf, $template);
pdf_close($pdf);

$data = pdf_get_buffer($pdf);

header("Content-type: application/pdf");
header("Content-disposition: inline; filename=test.pdf");
header("Content-length: " . strlen($data));

echo $data;

?>