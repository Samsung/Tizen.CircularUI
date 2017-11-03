#!/usr/bin/env bash

java -jar uml/plantuml.1.2017.18.jar uml/*.wsd

cat CircularUI.md > README.md
cat CircularUI.md > README.onepage.md

for x in part*.*.md
do
	title=$(sed -n '/# .*/s/# //p' $x)
	cat $x >> README.onepage.md
	echo "" >> README.onepage.md
	[[ ! -z $title ]] && echo "* [$title]($x)" >> README.md
done
