import ApiRepository from '../../data/api/ApiRepository'

export default class FileStorageService {
  constructor(private readonly apiRepository: ApiRepository) {}

  async uploadFile(file: File): Promise<unknown> {
    const formData = new FormData()
    formData.append('file', file)

    const response = await this.apiRepository.post<unknown>('/file/upload', undefined, formData)

    return response
  }
}
