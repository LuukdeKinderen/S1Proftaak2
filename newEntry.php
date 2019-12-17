<?php

foreach($_REQUEST as $key => $value) {
	if($key == "uid") {
		$bufferUid = $value;
	}

	if($key == "bal") {
		$bufferBal = $value;
	}

	if($key == "in") {
		$bufferIn = $value;
	}
}

$con = mysqli_connect("localhost", "root", "Welkom01!" , "Frowrail_Userdata");
if (mysqli_connect_errno()) {
	echo "Failed to connect to Database: " . mysqli_connect_errno();
}

mysqli_query($con,"INSERT INTO `Userdata` (`UID`, `BALANCE`, `CHECKIN`) VALUES ($bufferUid, $bufferBal, $bufferIn)");

$result = mysqli_query($con,"SELECT * FROM Userdata");
while($row = mysqli_fetch_array($result)) {
	if($row['UID'] == $bufferUid){
		$returnValue1 = $row['BALANCE'];
		$returnValue1 = $row['CHECKIN'];
	}
}

$returnPayload = strval(strlen($returnValue1)) . "_" . strval($returnValue1) . "#" . strval($returnValue2);
$returnPayload = "**TByte*" . "##" . $returnPayload; 
echo $returnPayload;

#	**TByte*##3_bal#1


?>
