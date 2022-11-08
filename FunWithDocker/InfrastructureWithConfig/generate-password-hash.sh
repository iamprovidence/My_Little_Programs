#!/bin/bash

read -p "Enter password: " password

PWD_HEX=$(echo -n $password | xxd -p)
SALT="908D C60A" 
HEX="$SALT $PWD_HEX"
SHA256=$(echo -n $HEX | xxd -r -p | sha256sum)
echo "908D C60A $SHA256" | xxd -r -p | base64 

read
