# Immutable Unity SDK
The Immutable Unity SDK package provides access to ImmutableX API endpoints from within Unity projects using the Immutable Core SDK in C#.

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

### 1. GetListedAssets
This example shows how to fetch assets and apply filters to get results.

Load ListedAssets.unity as the active scene. If you do not have Text Mesh Pro set up, you will recieve a popup and can click to import TMP essentials.
Some assets listed on IMX can use ascii characters not found in the font packs, to avoid Text Mesh Pro throwing warnings you can disable the `Disable warnings` flag in TMP settings scriptable object.
The TMP settings by default is stored in `Assets/TextMesh Pro/Resources`

Hit play and you should get an empty list with input fields and a FETCH button. Clicking the fetch button will fetch assets from immutable sandbox and display them in the list, this may take a few seconds to populate.

Below you can modify several query parameters to view assets on ImmutableX marketplace:
Name will allow you to filter by NFT name.
Collection Address will allow you to filter by specific collections.
Page Size will allow you to choose how many items to fetch, default is 50.
User ID allows you to filter by specific user assets.
Environment lets you choose between sandbox or main net assets.

### 2. GetStarkKeys
This example shows how to fetch stark keys from a user wallet ID for operations.

Load GetStarkKeys.unity and press play.

You will receive an input field and fetch button, you will need to enter a user ID on IMX, click fetch and the user data field will populate with account keys for that user.
You have access to an environment toggle to fetch on sandbox or main net.

### 3. GetBalance
This example shows how to get the balances from a user wallet, and filter by token type.

Load GetBalance.unity and press play.

You have a user wallet address input field for a public wallet key, and an environment dropdown if the balances are on sandbox or main net environments. Clicking Get Balance will return if a user has balances in that environment, and if so the token dropdown will populate with what tokens are in the wallet. Different tokens to view can be selected through the dropdown
