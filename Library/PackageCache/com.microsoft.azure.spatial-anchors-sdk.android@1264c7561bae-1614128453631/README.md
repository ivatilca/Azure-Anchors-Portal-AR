# Unity Android SDK for Azure Spatial Anchors

This guide will show you how to set up your Unity project to use the Unity Android SDK for Azure Spatial Anchors.


## Configure the mainTemplate.gradle file

1. Go to **Edit > Project Settings > Player**.
2. In the Inspector Panel for **Player Settings**, select the Android icon.
3. Under the Build section, check the **Custom Main Gradle Template** checkbox to generate a custom gradle template at `Assets\Plugins\Android\mainTemplate.gradle`.
4. Open your `mainTemplate.gradle` file in a text editor.
5. In the `dependencies` section, paste the following dependencies:
    ```gradle
    implementation('com.squareup.okhttp3:okhttp:[3.11.0]')
    implementation('com.microsoft.appcenter:appcenter-analytics:[1.10.0]')
    ```
6. When it's all done, your `dependencies` section should look something like this:
    ```gradle
    dependencies {
        implementation fileTree(dir: 'libs', include: ['*.jar'])
        implementation('com.squareup.okhttp3:okhttp:[3.11.0]')
        implementation('com.microsoft.appcenter:appcenter-analytics:[1.10.0]')
    **DEPS**}
    ```


## Configure the Spatial Anchors account information

1. Navigate to `Assets/AzureSpatialAnchors.SDK/Resources`.
2. Select **SpatialAnchorConfig**. This file will have been automatically generated, if it did not already exist.
3. Set `Spatial Anchors Account Id` to the value provided by the Azure portal.
4. Set `Spatial Anchors Account Key` to the value provided by the Azure portal.
5. Set `Spatial Anchors Account Domain` to the value provided by the Azure portal.

**NOTE:** The **SpatialAnchorConfig** file can be used in your own projects to share service credentials across scenes. When this file is used, you do not need to set these values on each **SpatialAnchorManager** in each scene. It's also possible to ignore this file in source control to avoid checking credentials into your repository.


## Building and deploying

1. Open **Build Settings** by selecting **File > Build Settings**.
2. Under **Scenes in Build**, ensure all the scenes have a check mark next to them.
3. Select your device in **Run Device** and then select **Build and Run**. You'll be asked to save an `.apk` file, which you can pick any name for.
4. After you've picked a name for the `.apk` file, your Unity application will finish building and deploying to your device.

