<div align="center">
  <p>
    <a  href="https://docs.x.immutable.com/docs">
      <img src="https://cdn.dribbble.com/users/1299339/screenshots/7133657/media/837237d447d36581ebd59ec36d30daea.gif" width="280"/>
    </a>
  </p>
</div>

# Immutable Unity SDK
The Immutable Unity SDK package provides access to ImmutableX API endpoints from within Unity projects using the Immutable Core SDK C#.

## Prerequisites
### Unity 2022.2
This package is in development and currently requires Unity 2022.2
Samples require TextMeshPro to use interactive UI examples.
The Immutable Core SDK C# requires several third party .DLL files that have been added to the plugins folder.

## Getting Started
This package is in development and currently contains functionality to read from ImmutableX API endpoints.

### Installation

Download the `.unitypackage` file from the Releases page and drag it into the `Assets/` window of an open project in the Unity Editor. 
Alternatively, you can double click the Unity package file to open it and then follow the UPM installation instructions.

### Registering with the Unity VS Team

It's highly recommended to register your Immutable Unity SDK installation with the Unity Verified Solutions team in case support is required.

To do so, from within the Unity Editor select `Immutable > Immutable Unity SDK` from the top navigation window. In the window that opens, enter your 
email address and click submit.

## Examples

Two samples are provided to showcase some of the functionality such as fetching assets on IMX and getting user data.
These examples can be found in `ImmutableSDK/Samples`.

### 1. GetListedAssets
This example shows how to fetch assets and apply filters to get results.

Load ListedAssets.unity as the active scene. If you do not have Text Mesh Pro set up, you will recieve a popup and can click to import TMP essentials.

Hit play and you should get an empty list with input fields and a FETCH button. Clicking the fetch button will fetch assets from immutable sandbox and display them in the list, this may take a few seconds to populate.

Below you can modify several query parameters to view assets on ImmutableX marketplace:
Name will allow you to filter by NFT name.
Collection Address will allow you to filter by specific collections.
Page Size will allow you to choose how many items to fetch, default is 50.
User ID allows you to filter by specific user assets.
Environment lets you choose between sandbox or main net assets.

### 2. GetStarkKeys
This examples shows how to fetch stark keys from a user wallet ID for operations.

Load GetStarkKeys.unity and press play.

You will receive an input field and fetch button, you will need to enter a user ID on IMX, click fetch and the user data field will populate with account keys for that user.
You have access to an environment toggle to fetch on sandbox or main net.

## Local Development & Contributions

If you'd like to contribute to this package or run the project locally, simply clone the repository 
and import the project into Unity.

See [CONTRIBUTING.md](https://github.com/immutable/imx-unity-sdk/blob/main/CONTRIBUTING.md) for details on PR expectations.

## Roadmap
<div>
      <img src="https://user-images.githubusercontent.com/96668470/217699376-86d904c2-3355-4e9a-bb34-feae034e8494.png" width="1200"/>
</div>
