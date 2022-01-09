# %%
import pandas as pd
import numpy as np
import json

data = pd.read_excel("初版卡牌导出.xlsx")

data

# %%
json_list = []

for _,card in data.iterrows():
    card_dict = {}
    card_dict["cardID"] = card.CardID
    card_dict["cardType"] = card.CardType
    card_dict["cardName"] = card.CardName
    card_dict["cardName_ZH"] = card.CardName_zh
    card_dict["cardInfo"] = card.CardInfo.split("\n")
    
    if card.CardType != "spellCard":
        unitinfo_dict = {}
        unitinfo_dict["HP"] = card.HP
        unitinfo_dict["ATK"] = card.ATK
        unitinfo_dict["ATKElement"] = card.ATKElement
        unitinfo_dict["selfElement"] = card.SelfElement
        card_dict["UnitInfo"] = unitinfo_dict
        if card.CardType == "charaCard":
            card_dict["CardCharge"] = card.ChargeInfo
    json_list.append(card_dict)

json_dict = {}
json_dict["cardData"] = json_list

data_json = json.dumps(json_dict)

print(json_list)

with open("data.json","w",encoding=utf-8) as f_w:
    f_w.write(data_json)    

# %%



