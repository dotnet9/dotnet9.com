using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shouldly;
using Volo.Abp.BlobStoring;
using Xunit;

namespace Dotnet9.Files;

public class AbpBlobStoringTencentCloudTests : Dotnet9DomainTestBase
{
	private readonly IBlobContainer _blobContainer;

	public AbpBlobStoringTencentCloudTests()
	{
		_blobContainer = GetRequiredService<IBlobContainer>();
	}

	[Fact]
	public async Task Should_Upload_File_Success()
	{
		// Arrange
		var bytes = new byte[] { 0x01, 0x02 };

		// Act
		await _blobContainer.SaveAsync("TestFile1", bytes);

		// Assert
		var fileStream = await _blobContainer.GetAsync("TestFile1");
		fileStream.Length.ShouldBe(2);
	}

	[Fact]
	public async Task File_Should_Exists()
	{
		// Arrange
		var bytes = new byte[] { 0x01, 0x02 };

		// Act
		await _blobContainer.SaveAsync("TestFile2", bytes);

		// Assert
		var isExists = await _blobContainer.ExistsAsync("TestFile2");
		isExists.ShouldBe(true);
	}

	[Fact]
	public async Task File_Should_Deleted()
	{
		// Arrange
		var bytes = new byte[] { 0x01, 0x02 };

		// Act
		await _blobContainer.SaveAsync("TestFile3", bytes);

		// Assert
		var response = await _blobContainer.DeleteAsync("TestFile3");
		response.ShouldBe(true);

		var isExist = await _blobContainer.ExistsAsync("TestFile3");
		isExist.ShouldBe(false);
	}
}