using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterBasicDataContainer : ISimpleContainer
{
    public List<CharacterBasicData> characterBasicDatas { get; private set; } = new();
    public void Regist(CharacterBasicData characterBasicData) => characterBasicDatas.Add(characterBasicData);
    public List<CharacterBasicData> Get() => characterBasicDatas.ToList();
}
