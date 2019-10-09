# -*- coding: utf-8 -*-
###
#
###

import os
import csv

CURDIR = os.getcwd()
SRC_DIR  = CURDIR + "/SRC/" #"D:/Nimei/ConsoleApp1/ConsoleApp1/Configs/SRC/"
DST_CS_DIR = CURDIR + "/../../GameContent/ConfigSystem/" #"D:/Nimei/ConsoleApp1/ConsoleApp1/Configs/DST_CS/"
DST_CSV_DIR = CURDIR + "/../../../../Resources/TextAssets/" #"D:/Nimei/ConsoleApp1/ConsoleApp1/Configs/DST_Configs/"

RES_DATA_SUFFIX = ".csv"
TARGET_FILE_SUFFIX = ".cs"
TARGET_DATA_SUFFIX = ".csv"

PROPERTY_NAMES_ROW = 0
PROPERTY_TYPES_ROW = 1
PROPERTY_COMMENT_ROW = 2

TABLE_SPACE = '    '

TYPE_BOOL = 'BOOL'
TYPE_INT = 'INT'
TYPE_STRING = 'STRING'
TYPE_FLOAT = 'FLOAT'

TYPE_LIST_BOOL = 'LIST_BOOL'
TYPE_LIST_INT = 'LIST_INT'
TYPE_LIST_FLOAT = 'LIST_FLOAT'
TYPE_LIST_STRING = 'LIST_STRING'


def GetFilesInDir(path):
	print('$ start get files in dir. ' + path)

	filesList = list()
	for folderName, subFolders, fileNames in os.walk(path):
		print('$ the current folder is: ' + folderName)
		for subFolder in subFolders:
			print('$SUBFOLDER of ' + folderName + ': ' + subFolder)
		for fileName in fileNames:
			print('$ FILE inside ' + folderName + ': ' + os.path.abspath(folderName + '/' + fileName))
			if fileName.endswith(RES_DATA_SUFFIX):
				filesList.append(os.path.abspath(folderName + '/' + fileName))
		print('')

	return filesList

def GetFileContent(path):
	print('$ start get file content.')
	file = open(path, 'r', encoding='UTF-8')
	reader = csv.reader(file)
	data = list(reader)
	print(data)

	file.close()

	return data

def ConvertToCS(fileName, data):
	print('$ start convert to cs.')

	#print(data[PROPERTY_NAMES_ROW])
	#print(data[PROPERTY_TYPES_ROW])
	#print(data[PROPERTY_COMMENT_ROW])

	srcname = os.path.basename(fileName)
	dstname = srcname.replace(RES_DATA_SUFFIX, '')

	content = 'using System;\n\n'
	content += 'namespace Config {\n'
	content += TABLE_SPACE + 'class '+ dstname + ' {\n'
	for index in range(len(data[PROPERTY_NAMES_ROW])):
		if data[PROPERTY_TYPES_ROW][index] == TYPE_BOOL:
			content += TABLE_SPACE * 2 + 'public bool ' + data[PROPERTY_NAMES_ROW][index] + ';\n'
		elif data[PROPERTY_TYPES_ROW][index] == TYPE_INT:
			content += TABLE_SPACE * 2 + 'public int ' + data[PROPERTY_NAMES_ROW][index] + ';\n'
		elif data[PROPERTY_TYPES_ROW][index] == TYPE_STRING:
			content += TABLE_SPACE * 2 + 'public string ' + data[PROPERTY_NAMES_ROW][index] + ';\n'
		elif data[PROPERTY_TYPES_ROW][index] == TYPE_FLOAT:
			content += TABLE_SPACE * 2 + 'public float ' + data[PROPERTY_NAMES_ROW][index] + ';\n'

	content += TABLE_SPACE + '}\n'
	content += '}\n'

	dstname = DST_CS_DIR + dstname + TARGET_FILE_SUFFIX
	return [dstname, content]

def ConstuctCSLoader(fileName, data):
	print("$ start construct cs loader.")
	srcname = os.path.basename(fileName)
	classname = srcname.replace(RES_DATA_SUFFIX, '')
	dstname = classname + 'Loader'

	propertiesCount = len(data[PROPERTY_NAMES_ROW])

    ##len(data[PROPERTY_NAMES_ROW])

	content = 'using System;\nusing System.Collections.Generic;\nusing System.IO;\n\n'
	content += 'namespace Config {\n'
	content += TABLE_SPACE + 'class '+ dstname + ' {\n'

	temp = 'Dictionary<int, ' + classname + '> '
	content += TABLE_SPACE * 2 + 'public ' + temp + 'Datas = new ' + temp +'();\n\n'

	content += TABLE_SPACE * 2 + 'public ' + temp + 'LoadConfigData(string str) {\n'

	content += TABLE_SPACE * 3 + 'string[] periods = str.Split(\'\\n\');\n'

	content += TABLE_SPACE * 3 + 'int index = 0;\n'
	
	content += TABLE_SPACE * 3 + 'while (index < periods.Length) {\n'

	content += TABLE_SPACE * 4 + 'string[] split = periods[index].Split(\',\');\n'
	content += TABLE_SPACE * 4 + 'if (split.Length == ' + str(propertiesCount) + ') {\n'
	content += TABLE_SPACE * 5 + classname + ' data = new ' + classname + '();\n'

	for index in range(propertiesCount):
		if data[PROPERTY_TYPES_ROW][index] == TYPE_BOOL:
			content += TABLE_SPACE * 5 + 'bool.TryParse(split['+ str(index) +'], out data.' + data[PROPERTY_NAMES_ROW][index] + ');\n'
		elif data[PROPERTY_TYPES_ROW][index] == TYPE_INT:
			content += TABLE_SPACE * 5 + 'int.TryParse(split['+ str(index) +'], out data.' + data[PROPERTY_NAMES_ROW][index] + ');\n'
		elif data[PROPERTY_TYPES_ROW][index] == TYPE_STRING:
			content += TABLE_SPACE * 5 + 'data.' + data[PROPERTY_NAMES_ROW][index] + '= split['+ str(index) +'];\n'
		elif data[PROPERTY_TYPES_ROW][index] == TYPE_FLOAT:
			content += TABLE_SPACE * 5 + 'float.TryParse(split['+ str(index) +'], out data.' + data[PROPERTY_NAMES_ROW][index] + ');\n'
	content += TABLE_SPACE * 5 + 'Datas.Add(data.ID, data);\n'
	content += TABLE_SPACE * 4 + '}\n'
	content += TABLE_SPACE * 4 + 'index++;\n'	
	content += TABLE_SPACE * 4 + '}\n'
	content += TABLE_SPACE * 3 + 'return Datas;\n'
	
	content += TABLE_SPACE * 2 + '}\n'

	content += TABLE_SPACE * 2 + 'public ' + classname + ' GetDataByID(int id) {\n'
	content += TABLE_SPACE * 3 + 'if (Datas.ContainsKey(id)) { \n'
	content += TABLE_SPACE * 4 + 'return Datas[id];\n'
	content += TABLE_SPACE * 3 + '}\n'
	content += TABLE_SPACE * 3 + 'return null;\n'
	content += TABLE_SPACE * 2 + '}\n'

	content += TABLE_SPACE + '}\n'
	content += '}\n'

	dstname = DST_CS_DIR + dstname + TARGET_FILE_SUFFIX
	return [dstname, content]	

def ConvertToDstCSV(fileName, data):
	print('$ start convert to csv.')
	content = list()

	for index in range(len(data)):
		if index > PROPERTY_COMMENT_ROW:
			content.append(data[index])

	str = 'abc'
	print(str.replace('a', 'c'))
	
	dstFileName = os.path.basename(fileName)
	print(dstFileName)

	dstFileName = dstFileName.replace(RES_DATA_SUFFIX, TARGET_DATA_SUFFIX)

	dstname = DST_CSV_DIR + os.path.basename(dstFileName)

	return [dstname, content]

def WriteToTargetFile(fileInfo):
	print('$ start write to file.')
	#print(fileInfo[0])

	if not fileInfo:
		return

	file = open(fileInfo[0], 'w')

	file.write(fileInfo[1]);
	file.close()

def WriteToCsvFile(fileInfo):
	print('$ start write to csv file.')
    
    #fileInfo[0] = fileInfo[0].replace(RES_DATA_SUFFIX, '')
        #TARGET_DATA_SUFFIX)
	file = open(fileInfo[0], 'w')
	writer = csv.writer(file, lineterminator='\n')
    #writer.lineterminator = ''
	for row in fileInfo[1]:
		writer.writerow(row)

	file.close()

def Convert():
	print('$ start convert.')
	filesList = GetFilesInDir(SRC_DIR)

	for file in filesList:
		#filepath = os.path.abspath(file)
		print('$ file: ' + file)
		data = GetFileContent(file)
		csFileInfo = ConvertToCS(file, data)
		WriteToTargetFile(csFileInfo)
		csLoaderInfo = ConstuctCSLoader(file, data)
		WriteToTargetFile(csLoaderInfo)
		dstCSVFileInfo = ConvertToDstCSV(file, data)		
		WriteToCsvFile(dstCSVFileInfo)

	print('$ convert done.')

Convert()
