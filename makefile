DATABASE = src/Data/Database/
API = src/APIKeys.cs
OBJ = src/obj/
bin = src/bin/


run :
	dotnet "run" "--project" "DiscordBot.sln"

build :
	@touch $(API)
	@mkdir $(DATABASE)
	@echo '{ }' >> $(DATABASE)/users.json
	@echo '{ }' >> $(DATABASE)/googleTokens.json
	@echo 'File structure succesfully created.'
	@echo 'Please insert you google drive API credentials into the right folder: $(DATABASE)'

clean :
	@rm -rf $(DATABSE)
	@rm -rf $(BIN)
	@rm -rf $(OBJ)
	@rm $(API)

rebuild : clean build
