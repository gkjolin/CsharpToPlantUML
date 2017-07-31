# CsharpToPlantUML
C#のコードを PlantUMLに変換したいぜ☆（＾～＾）！

# 変換例

変換前

```
        /// <summary>
        /// スコアのスクリプトのキャッシュ
        /// </summary>
        static Score score_cache;
```

変換後

```
- score_cache : Score {static} 'スコアのスクリプトのキャッシュ'
```

- ステレオタイプは型の後ろに付けます
