DATABASE = src/Data/Database
API = src/APIKeys.cs


build :
	@touch $(API)
	@mkdir $(DATABASE)
	@cd $(DATABASE)
	@echo '{ }' >> users.json
	@echo '{ }' >> googleTokens.json
	echo 'File structure succesfully created. \nPlease insert you google drive API credentials into the right folder: $(DATABASE)'
	@cd ../../..
clean :
	@rm -rf $(DATABSE)
	@rm -f $(API)

rebuild : clean build
