# Immutable Unity SDK
The Immutable Unity SDK package provides access to ImmutableX API endpoints from within Unity projects using the [Immutable Core SDK in C#](https://docs.x.immutable.com/sdk-docs/core-sdk-csharp/overview).

Currently, the C# Core SDK provides this ***read-only*** functionality.
* Assets: Get & List
* Balances: Get & List
* Collections: Get & List & List with filters
* Exchanges: Get & List
* Metadata: Get
* Mints: Get & List
* NFT Checkout Primary: Get & List transactions, Get currencies
* Deposits: Get & List
* Withdrawals: Get & List
* Transfers: Get & List
* Trades: Get & List
* Orders: Get & List
* Users: Get
* Tokens: Get & List

A roadmap pdf of remaining features is in the package root folder for upcoming development.
<div>
      <img src="https://user-images.githubusercontent.com/96668470/217699376-86d904c2-3355-4e9a-bb34-feae034e8494.png" width="1200"/>
</div>

## Additional Documentation
* Get started building on ImmutableX with the [Developer docs](https://docs.x.immutable.com/docs/welcome/)
* Check out the [API reference documentation](https://docs.x.immutable.com/reference)
* For the C# Core SDK reference documentation, please see [here](https://docs.x.immutable.com/sdk-docs/core-sdk-csharp/overview)

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

To do so, from within the Unity Editor select `Immutable > Immutable Unity SDK` from the top navigation window after installing the plugin. In the window that opens, enter your 
email address and click submit.


## Examples

Three samples are provided to showcase some of the functionality such as fetching assets on IMX and getting user data.
These examples can be found in `ImmutableSDK/Samples`.
Demos use the below example wallets to auto fill that can be used for testing:
* 0xF652A8bCb0Df65AE5fEd91DECaA8B591caeD1e1c
* 0x5A7CB0ba4D94b6B08B837E4e97A90b9F2400C80D
* 0x2d0ad946788938B9044ed72b1C464e1e9bb9d401

### 1. GetListedAssets
This example shows how to fetch assets and apply filters to get results.

Load ListedAssets.unity as the active scene. If you do not have Text Mesh Pro set up, you will recieve a popup and can click to import TMP essentials.
Some assets listed on IMX can use ascii characters not found in the font packs, to avoid Text Mesh Pro throwing warnings you can disable the `Disable warnings` flag in TMP settings scriptable object.
The TMP settings by default is stored in `Assets/TextMesh Pro/Resources`

Hit play and you should recieve pre populated inputs to view a Gods Unchained player wallet. Click fetch to view all the NFTs they own for that collection in their wallet.

Below you can modify several query parameters to view assets on ImmutableX marketplace:
Name will allow you to filter by NFT name.
Collection Address will allow you to filter by specific collections.
Page Size will allow you to choose how many items to fetch, default is 50.
User ID allows you to filter by specific user assets.
Environment lets you choose between sandbox or main net assets.

### 2. GetStarkKeys
This example shows how to fetch stark keys from a user wallet ID for operations.

Load GetStarkKeys.unity and press play.

You will receive an input field and fetch button alongside a pre-populated wallet address on main net. Press Get Stark Keys to view the L2 keys for this wallet.
You have access to an environment toggle to fetch on sandbox or main net.

### 3. GetBalance
This example shows how to get the balances from a user wallet, and filter by token type.

Load GetBalance.unity and press play.

A user wallet will be pre-populated, clicking get balance will return their ETH balance on main net as wei.
