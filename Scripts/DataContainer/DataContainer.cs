using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class DataContainer
{
    public ItemDataContainer itemDataContainer { get; private set; } = new ItemDataContainer();

    public QuestDataContainer questDataContainer { get; private set; } = new QuestDataContainer();

    public NpcDataContainer npcDataContainer { get; private set; } = new NpcDataContainer();

    public ExpValueDataContainer expValueDataContainer { get; private set; } = new ExpValueDataContainer();

    public WaveDataContainer waveDataContainer { get; private set; } = new WaveDataContainer();

    public PatternDataContainer patternDataContainer { get; private set; } = new PatternDataContainer();

    public StringDataContainer stringDataContainer { get; private set; } = new StringDataContainer();

    public SkillDataContainer skillDataContainer { get; private set; } = new SkillDataContainer();

    public EffectDataContainter effectDataContainer { get; private set; } = new EffectDataContainter();

    public SoundDataContainer soundDataContainer { get; private set; } = new SoundDataContainer();

    public CharacterBasicDataContainer characterBasicDataContainer { get; private set; } = new CharacterBasicDataContainer();

    public PatchDataContainer patchDataContainer { get; private set; } = new PatchDataContainer();

    public ItemContainer itemContainer { get; private set; } = new ItemContainer();

    public void Push(ScriptableObject table)
    {
        RegistItemDataContainer<ItemData>(table);
        RegistQuestDataContainer<QuestData>(table);
        RegistNpcDataContainer<NpcData>(table);
        RegistExpValueDataContainer<ExpValueData>(table);
        RegistWaveDataContainer<WaveData>(table);
        RegistPatternDataContainer<PatternData>(table);
        RegistStringDataContainer<StringData>(table);
        RegistSkillDataContainer<SkillData>(table);
        RegistEffectDataContainter<EffectData>(table);
        RegistSoundDataContainer<SoundData>(table);
        RegistCharacterBasicDataContainer<CharacterBasicData>(table);
    }

    public void RegistItemDataContainer<T>(ScriptableObject table) where T : ItemData
    {
        if (table.IsRelativeClassOf<T>(typeof(T)))
        {
            var data = table as T;
            itemDataContainer.Regist(data);
        }
    }

    public void RegistQuestDataContainer<T>(ScriptableObject table) where T : QuestData
    {
        if (table.IsRelativeClassOf<T>(typeof(T)))
        {
            var data = table as QuestData;
            questDataContainer.Regist(data);
        }
    }

    public void RegistNpcDataContainer<T>(ScriptableObject table) where T : NpcData
    {
        if (table.IsRelativeClassOf<T>(typeof(T)))
        {
            var data = table as NpcData;
            npcDataContainer.Regist(data);
        }
    }

    public void RegistExpValueDataContainer<T>(ScriptableObject table) where T : ExpValueData
    {
        if (table.IsRelativeClassOf<T>(typeof(T)))
        {
            var data = table as ExpValueData;
            expValueDataContainer.Regist(data);
        }
    }

    public void RegistWaveDataContainer<T>(ScriptableObject table) where T : WaveData
    {
        if (table.IsRelativeClassOf<T>(typeof(T)))
        {
            var data = table as WaveData;
            waveDataContainer.Regist(data);
        }
    }

    public void RegistPatternDataContainer<T>(ScriptableObject table) where T : PatternData
    {
        if (table.IsRelativeClassOf<T>(typeof(T)))
        {
            var data = table as PatternData;
            patternDataContainer.Regist(data);
        }
    }

    public void RegistStringDataContainer<T>(ScriptableObject table) where T : StringData
    {
        if (table.IsRelativeClassOf<T>(typeof(T)))
        {
            var data = table as StringData;
            stringDataContainer.Regist(data);
        }
    }

    public void RegistSkillDataContainer<T>(ScriptableObject table) where T: SkillData
    {
        if (table.IsRelativeClassOf<T>(typeof(T)))
        {
            var data = table as SkillData;
            skillDataContainer.Regist(data);
        }
    }

    public void RegistEffectDataContainter<T>(ScriptableObject table) where T: EffectData
    {
        if (table.IsRelativeClassOf<T>(typeof(T)))
        {
            var data = table as EffectData;
            effectDataContainer.Regist(data);
        }
    }
    public void RegistSoundDataContainer<T>(ScriptableObject table) where T : SoundData
    {
        if (table.IsRelativeClassOf<T>(typeof(T)))
        {
            var data = table as SoundData;
            soundDataContainer.Regist(data);
        }
    }
    
    public void RegistCharacterBasicDataContainer<T>(ScriptableObject table) where T : CharacterBasicData
    {
        if (table.IsRelativeClassOf<T>(typeof(T)))
        {
            var data = table as CharacterBasicData;
            characterBasicDataContainer.Regist(data);
        }
    }
}
