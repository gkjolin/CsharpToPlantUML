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
{static} - score_cache; : Score : '<summary> スコアのスクリプトのキャッシュ </summary> '
```

