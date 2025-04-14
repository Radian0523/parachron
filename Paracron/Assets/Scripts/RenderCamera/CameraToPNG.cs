using UnityEngine;
using System.IO;

public class CameraToPNG : MonoBehaviour
{
    public Camera renderCamera;     // キャプチャしたいカメラ
    public int width = 512;         // 保存する画像の幅
    public int height = 512;        // 保存する画像の高さ

    void Update()
    {
        // Pキーを押すとPNG保存を実行
        if (Input.GetKeyDown(KeyCode.P))
        {
            SaveTransparentPNG();
        }
    }

    public void SaveTransparentPNG()
    {
        // カメラの背景をピンク色に設定
        renderCamera.clearFlags = CameraClearFlags.SolidColor;
        renderCamera.backgroundColor = new Color(1f, 0.4f, 0.7f, 1f); // ピンク色 (RGB: 1, 0.4, 0.7)

        // RenderTextureの準備（アルファ付き）
        RenderTexture rt = new RenderTexture(width, height, 24, RenderTextureFormat.ARGB32);
        rt.Create();  // RenderTextureの作成を確実にする
        renderCamera.targetTexture = rt;

        // 描画を実行
        RenderTexture.active = rt;
        renderCamera.Render();

        // RenderTextureの内容をTexture2Dに読み込む
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGBA32, false);
        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        tex.Apply();

        // 後始末：カメラのターゲットとアクティブRenderTextureを解放
        renderCamera.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);

        // PNGファイルとして保存
        byte[] bytes = tex.EncodeToPNG();
        string path = Application.dataPath + "/TransparentImage.png";
        File.WriteAllBytes(path, bytes);

        // 完了メッセージ
        Debug.Log("✅ Saved transparent PNG to: " + path);
    }
}
