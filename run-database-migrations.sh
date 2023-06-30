#!/bin/bash

DatabaseFile=$(pwd)/RssReader.db

# Absolutely unhinged default parameter syntax.
# This will use the argument if provided,
# otherwise default to the value.
SchemaDirectory=${1-./src/RssReader/Schema}

echo "Running database migration scripts in $SchemaDirectory against $DatabaseFile."
pushd $SchemaDirectory > /dev/null

for file in *
do
	echo $file
	sqlite3 $DatabaseFile < $file
	echo "    Done"
done
echo "Finished database migration."
