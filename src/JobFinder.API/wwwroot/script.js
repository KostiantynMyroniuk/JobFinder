{
    const API_BASE_URL = 'https://localhost:7056';
    const ENDPOINT = '/jobs/Vacancies';
    let currentPage = 1;
    const pageSize = 10;

    const listContainer = document.getElementById('vacancy-list');
    const loader = document.getElementById('loader');
    const paginationContainer = document.getElementById('pagination');
    const btnPrev = document.getElementById('btn-prev');
    const btnNext = document.getElementById('btn-next');
    const pageInfo = document.getElementById('page-info');

    const icons = {
        bookmark: `<svg viewBox="0 0 24 24"><path stroke-linecap="round" stroke-linejoin="round" d="M5 5a2 2 0 012-2h10a2 2 0 012 2v16l-7-3.5L5 21V5z" /></svg>`,
        hide: `<svg viewBox="0 0 24 24"><path stroke-linecap="round" stroke-linejoin="round" d="M18.364 18.364A9 9 0 005.636 5.636m12.728 12.728A9 9 0 015.636 5.636m12.728 12.728L5.636 5.636" /></svg>`
    };

    async function loadVacancies(page) {
        showLoading(true);
        try {
            const response = await fetch(`${API_BASE_URL}${ENDPOINT}?PageNumber=${page}&PageSize=${pageSize}`);
            if (!response.ok) throw new Error(`Status: ${response.status}`);
            const data = await response.json();

            renderVacancies(data.items);
            updatePagination(data);
            currentPage = page;
        } catch (error) {
            console.error(error);
            listContainer.innerHTML = `<p style="text-align:center; color: red;">Помилка: ${error.message}.<br>Перевір чи запущено сервер і чи правильний порт.</p>`;
        } finally {
            showLoading(false);
        }
    }

    function renderVacancies(items) {
        listContainer.innerHTML = '';

        if (!items || items.length === 0) {
            listContainer.innerHTML = '<p>Список порожній.</p>';
            return;
        }

        items.forEach(v => {
            const detailString = [
                v.workFormat,
                v.location,
                "Продукт",
                `${v.experience}+ років досвіду`
            ].filter(Boolean).join(' &middot; ');

            const initials = v.name ? v.name.slice(0, 2).toUpperCase() : 'VA';
            const timeAgo = formatTime(v.minutesAgo);

            const html = `
            <div class="vacancy-card">
                <div class="card-top-row">
                    <div class="logo-placeholder">${initials}</div>
                    <span class="company-name">Data Science UA</span>
                    <span class="separator">&middot;</span>
                    <span>0 переглядів</span>
                    <span class="separator">&middot;</span>
                    <span>0 відгуків</span>
                    <span class="separator">&middot;</span>
                    <span class="post-time">${timeAgo}</span>
                </div>

                <a href="#" class="vacancy-title">${v.name}</a>

                <div class="details-row">
                    ${detailString}
                </div>

                <div class="english-row">
                    ${v.englishLevel ? `English: ${v.englishLevel}` : ''}
                </div>

                <div class="desc-text">
                    About us: ${v.description} <span class="more-link">Більше</span>
                </div>

                <div class="actions-footer">
                    <button class="action-btn">
                        ${icons.bookmark} Зберегти
                    </button>
                    <button class="action-btn">
                        ${icons.hide} Сховати
                    </button>
                </div>
            </div>
            `;

            listContainer.insertAdjacentHTML('beforeend', html);
        });
    }

    function updatePagination(data) {
        if (!data) return;
        pageInfo.textContent = `${data.pageNumber} з ${data.totalPages}`;
        btnPrev.disabled = !data.hasPreviousPage;
        btnNext.disabled = !data.hasNextPage;
        paginationContainer.style.display = 'flex';
    }

    function formatTime(minutes) {
        if (minutes < 60) {
            return minutes <= 0 ? "щойно" : `${minutes}хв`;
        }

        if (minutes < 1440) {
            const hours = Math.floor(minutes / 60);
            return `${hours}год`;
        }

        const days = Math.floor(minutes / 1440);
        return `${days}дн`;
    }

    function showLoading(state) {
        if (loader) loader.style.display = state ? 'block' : 'none';
        if (listContainer) listContainer.style.display = state ? 'none' : 'block';
    }

    if (btnPrev) btnPrev.addEventListener('click', () => currentPage > 1 && loadVacancies(currentPage - 1));
    if (btnNext) btnNext.addEventListener('click', () => loadVacancies(currentPage + 1));

    document.addEventListener('DOMContentLoaded', () => loadVacancies(1));
}