const hkitNft = artifacts.require("./HkitNft.sol");

module.exports = function(deployer) {
  deployer.deploy(hkitNft,"HkitNft","KiepMeta1");
};
