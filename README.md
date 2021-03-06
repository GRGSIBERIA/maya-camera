maya-camera
===========

Mayaのような操作性のカメラです．

## 使い方
MayaCamera.csをメインのカメラにアサインしてください．
Mayaっぽくカメラの操作ができるようになります．

| 操作           | 内容     |
|:---------------|----------|
| Alt+左クリック | 回転     |
| Alt+右クリック | 拡大縮小 |
| Alt+中ボタン   | 移動     |

Alt+左右クリック同時押しでも移動ができます．

## NGUIのアセットについて
NGUIでカメラを操作するためのボタンを作りました．
ボタンを押しながらマウスを動かすとカメラを操作できます．
ファイルはmasterブランチではなく，NGUI-assetsブランチに置いています．

### アセットの使い方
./prefab/Camera Operation UI.prefabがアセットになっています．
MayaCamera.csをメインのカメラにアサインするのを確認し，
NGUIがインポートされた状態で当該のアセットをD&Dすればすぐに使えるはずです．
（ngui_distribution.unitypackageですぐに試すことができます）

こちらはおまけという扱いで，無保証となっています．ご注意ください．

### 利用した素材・フォント
ボタンは[取り放題.com](http://toriho-dai.com/)からお借りしました．

イメージフォントは小塚ゴシックLE及び[やさしさゴシック](http://www.fontna.com/blog/379/)を利用しています．いずれのライセンスも再利用が厳しいと思います．イメージフォントを利用したい場合は，実フォントをダウンロードないしはインストールし，[Bitmap Font Generator](http://www.angelcode.com/products/bmfont/)等でイメージフォント化してから利用してください．実フォントのライセンスにはご注意ください．

## 変数・プロパティ
MayaCameraクラスにはいくつか公開している変数・プロパティがあります．

### 変数
主にマウス操作の速度が変数として公開されています．

| 変数名      | 説明             |
|:------------|------------------|
| tumbleSpeed | 回転の速さ       |
| dollySpeed  | 拡大縮小の速さ   |
| trackSpeed  | 移動の速さ       |
| transitTime | カメラの移動時間 |

### プロパティ
主にマウスの位置・速度・加速度がプロパティとして公開されています．
変更はできないので注意してください．

| 変数名        | 説明           |
|:--------------|----------------|
| mousePosition | マウスの位置   |
| mouseSpeed    | マウスの速度   |
| mouseAccel    | マウスの加速度 |

## 関数
MayaCameraクラスにはいくつか公開されている関数があります．

### InvokeDolly() : void
右クリックでカメラの遠近を行う操作と同じです．
判定処理を飛ばして実行します．

### InvokeTumble() : void
中ボタンクリックでカメラの平行移動を行う操作と同じです．
判定処理を飛ばして実行します．

### InvokeTrack() : void
左クリックでカメラの被写体を中心とした回転を行う操作と同じです．
判定処理を飛ばして実行します．

### LookAtHere(Vector3 lookat, float timeTo) : void
LookAtHere関数を呼び出すと，カメラの注視点をlookatまで移動します．
移動はMayaCameraクラスのUpdateで行われるため，呼び出しは1度だけで結構です．

timeToは移動にかかる時間です．timeToは任意に指定できる変数です．
timeToが指定されていない場合，transitTimeが代わりに代入されます．

### GotoHere(Vector3 gotoHere, float timeTo) : void
GotoHere関数を呼び出すと，カメラの座標をgotoHereまで移動します．
移動はMayaCameraクラスのUpdateで行われるため，呼び出しは1度だけで結構です．

timeToは移動にかかる時間です．timeToは任意に指定できる変数です．
timeToが指定されていない場合，transitTimeが代わりに代入されます．

## カメラの挙動に関して
カメラの拡大縮小と移動の速さは，カメラと注視点の距離が離れているほど速くなります．
逆に，距離が近いほど遅くなるようになっています．
どの距離でも一定の速さを保ちたい場合は，CalculateCameraPhisics関数内のコードを以下のように書き換えてください．

```MayaCamera.cs
//log10VectorLength = Mathf.Log10(cameraToLookAtVector.sqrMagnitude) + 1f;
log10VectorLength = 1f
```

## ライセンス
修正BSDライセンスです．ソースコードを組み込んだことによる報告等は必要ありません．

## 連絡先
リポジトリのIssueからバグ，要望等を報告して頂ければ幸いです．

twitter: @grgsiberia
