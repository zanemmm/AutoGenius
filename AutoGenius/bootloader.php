<?php

if (!extension_loaded('curl')) {
    dl('curl');
}

require './php_library/HTTPClient.php';
require './php_library/Display.php';
require './php_library/Keyboard.php';
require './php_library/Mouse.php';

use AutoGenius\HTTPClient;
use AutoGenius\Display;
use AutoGenius\Keyboard;
use AutoGenius\Mouse;

if ($argc < 4) {
    echo '参数不足，需要指定服务端IP，端口和脚本路径' . PHP_EOL;
}

define('CWD', dirname($argv[3]));

$client = new HTTPClient($argv[1], (int)$argv[2]);
$display = new Display($client);
$keyboard = new Keyboard($client);
$mouse = new Mouse($client);

require $argv[3];

main($display, $keyboard, $mouse);
